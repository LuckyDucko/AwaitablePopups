using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AsyncAwaitBestPractices;

using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.PopupPages.Loader;

using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;

namespace AwaitablePopups.Services
{
	public class PopupService : IPopupService
	{
		private static volatile PopupService s_internalPopupService = null;
		private static IPopupNavigation s_popupNavigation = null;
		private static readonly object s_threadPadlock = new object();
		//private PopupPage TopPopupPage { get; set; }

		private PopupService(IPopupNavigation popupNavigation = null)
		{
			s_popupNavigation = popupNavigation ?? PopupNavigation.Instance;
		}

		//private void ResetTopPopupPage(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
		//{
		//    TopPopupPage = s_popupNavigation.PopupStack.Last();
		//}

		//private void RestrictPopAbility(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
		//{
		//    TopPopupPage = null;
		//}

		public static PopupService GetInstance(IPopupNavigation popupNavigation = null)
		{
			if (s_popupNavigation == null)
			{
				lock (s_threadPadlock)
				{
					if (s_popupNavigation == null)
					{
						s_internalPopupService = new PopupService(popupNavigation);
					}
				}
			}
			return s_internalPopupService;
		}

		public void PopAsync<TPopupType>() where TPopupType : PopupPage, new()
		{
			lock (s_threadPadlock)
			{
				var PotentialPages = s_popupNavigation.PopupStack.Where((PopupPage arg) => arg.GetType().IsEquivalentTo(typeof(TPopupType)));
				if (PotentialPages.Any())
				{
					s_popupNavigation.RemovePageAsync(PotentialPages.First()).SafeFireAndForget();
				}
			}
		}

		public TPopupPage CreatePopupPage<TPopupPage>()
			where TPopupPage : PopupPage, new()
		{
			return new TPopupPage();
		}

		public TPopupPage AttachViewModel<TPopupPage, TViewModel>(TPopupPage popupPage, TViewModel viewModel)
			where TPopupPage : PopupPage, IGenericViewModel<TViewModel>
			where TViewModel : BasePopupViewModel
		{
			popupPage.SetViewModel(viewModel);
			return popupPage;
		}

		public async Task ForceMinimumWaitTime(Task returnableTask, int millisecondsDelay)
		{
			Task initialTime = Task.Delay(millisecondsDelay);
			await Task.WhenAll(initialTime, returnableTask);
		}

		public async Task WrapTaskInLoader(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task PaddedTaskTime = ForceMinimumWaitTime(action, 1000);
			ConstructLoaderAndDisplay(PaddedTaskTime, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
			await PaddedTaskTime;
		}

		public async Task<TAsyncActionResult> WrapReturnableTaskInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			ConstructLoaderAndDisplay(action, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
			await action;
			return action.Result;
		}

		public async Task<TAsyncActionResult> WrapReturnableTCSInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			ConstructTCSSafeLoaderAndDisplay(action, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
			await action;
			return action.Result;
		}

		private void ConstructTCSSafeLoaderAndDisplay(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
			action.GetAwaiter().OnCompleted(() => Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal<LoaderPopupPage>()));
			if (!action.IsCompleted)
			{
				LoaderAttachAndPush(loaderWaiting).SafeFireAndForget();
			}
		}

		private void ConstructLoaderAndDisplay(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
			action.GetAwaiter().OnCompleted(() => Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal<LoaderPopupPage>()));
			if (!action.IsCompleted && action.Status != TaskStatus.WaitingForActivation)
			{
				LoaderAttachAndPush(loaderWaiting).SafeFireAndForget();
			}
		}

		private async Task LoaderAttachAndPush(LoaderViewModel loaderWaiting)
		{
			var popupModal = AttachViewModel(CreatePopupPage<LoaderPopupPage>(), loaderWaiting);
			await Device.InvokeOnMainThreadAsync(() => s_popupNavigation.PushAsync(popupModal));
		}

		public async Task<TReturnable> PushAsync<TViewModel, TPopupPage, TReturnable>(TViewModel modalViewModel)
			where TPopupPage : PopupPage, IGenericViewModel<TViewModel>, new()
			where TViewModel : PopupViewModel<TReturnable>
		{
			TPopupPage popupModal = AttachViewModel(CreatePopupPage<TPopupPage>(), modalViewModel);
			await s_popupNavigation.PushAsync(popupModal);
			return await modalViewModel.Returnable.Task;
		}

		private LoaderViewModel ConstructLoaderModal(Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			return new LoaderViewModel(this, reasonsForLoader)
			{
				LoaderColour = loaderColour,
				MainPopupColour = loaderPopupColour,
				TextColour = textColour,
				MillisecondsBetweenReasonSwitch = millisecondsBetweenReasons,
			};
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TSyncActionResult>(Func<TSyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(action);
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TSyncActionResult>(Func<TArgument1, TSyncActionResult> action, TArgument1 argument1, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TSyncActionResult>(Func<TArgument1, TArgument2, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1, argument2));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4, argument5));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}

		public async Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons = 2000)
		{
			Task<TSyncActionResult> actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4, argument5, argument6));
			return await WrapReturnableTaskInLoader(actionResult, loaderColour, loaderPopupColour, reasonsForLoader, textColour, millisecondsBetweenReasons);
		}
	}
}
