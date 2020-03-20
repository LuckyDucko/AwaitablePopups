using System;
using System.Collections.Generic;
using System.Linq;
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
        private PopupPage TopPopupPage { get; set; }

        private PopupService(IPopupNavigation popupNavigation = null)
        {
            s_popupNavigation = popupNavigation ?? PopupNavigation.Instance;
            s_popupNavigation.Popping += RestrictPopAbility;
            s_popupNavigation.Popped += ResetTopPopupPage;
            s_popupNavigation.Pushed += ResetTopPopupPage;
        }

        private void ResetTopPopupPage(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            using var popupStack = s_popupNavigation.PopupStack.GetEnumerator();
            while (popupStack.MoveNext())
            {
                TopPopupPage = popupStack.Current;
            }
        }

        private void RestrictPopAbility(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            TopPopupPage = null;
        }

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

        public void PopAsync()
        {
            lock (s_threadPadlock)
            {
                if (TopPopupPage != null)
                {
                    s_popupNavigation.RemovePageAsync(TopPopupPage).SafeFireAndForget();
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

        public async Task WrapTaskInLoader(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            ConstructLoaderAndDisplay(action, loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            await action;
        }


        public async Task<TAsyncActionResult> WrapReturnableTaskInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            ConstructLoaderAndDisplay(action, loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            await action;
            return action.Result;
        }

        private void ConstructLoaderAndDisplay(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            if (!action.IsCompleted)
            {
                LoaderAttachAndPush(loaderWaiting);
                action.GetAwaiter().OnCompleted(() => Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal()));
            }
        }

        private void LoaderAttachAndPush(LoaderViewModel loaderWaiting)
        {
            var popupModal = AttachViewModel(CreatePopupPage<LoaderPopupPage>(), loaderWaiting);
#pragma warning disable CS4014
            Device.BeginInvokeOnMainThread(() => s_popupNavigation.PushAsync(popupModal));
#pragma warning restore CS4014
        }

        /// <typeparam name="TViewModel">The ViewModel Type that is Attached to the Generic PopupPage</typeparam>
        /// <typeparam name="TPopupPage">The Generic Popupup that will be pushed onto the Navigation Stack</typeparam>
        /// <typeparam name="TReturnable">The Type that we expect to be return from the PopupPage through the generic ViewModel </typeparam>
        /// <param name="modalViewModel">The ViewModel that has been created</param>
        /// <returns>an Async Task that will wait for the PopupPage to return its value through its Viewmodel</returns>
        public async Task<TReturnable> PushAsync<TViewModel, TPopupPage, TReturnable>(TViewModel modalViewModel)
            where TPopupPage : PopupPage, IGenericViewModel<TViewModel>, new()
            where TViewModel : PopupViewModel<TReturnable>
        {
            TPopupPage popupModal = AttachViewModel(CreatePopupPage<TPopupPage>(), modalViewModel);
            await s_popupNavigation.PushAsync(popupModal);
            return await modalViewModel.Returnable.Task;
        }

        private LoaderViewModel ConstructLoaderModal(Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            return new LoaderViewModel(this, reasonsForLoader)
            {
                LoaderColour = loaderColour,
                MainPopupColour = loaderPopupColour,
                TextColour = textColour,
                MillisecondsBetweenReasonSwitch = 2000
            };
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TSyncActionResult>(Func<TSyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(action).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TSyncActionResult>(Func<TArgument1, TSyncActionResult> action, TArgument1 argument1, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TArgument2, TSyncActionResult>(Func<TArgument1, TArgument2, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1, argument2)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4, argument5)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }

        public TSyncActionResult WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour)
        {
            LoaderViewModel loaderWaiting = ConstructLoaderModal(loaderColour, loaderPopupColour, reasonsForLoader, textColour);
            Device.BeginInvokeOnMainThread(() => LoaderAttachAndPush(loaderWaiting));
            TSyncActionResult actionResult = Task.Run(() => action.Invoke(argument1, argument2, argument3, argument4, argument5, argument6)).Result;
            Device.BeginInvokeOnMainThread(() => loaderWaiting.SafeCloseModal());
            return actionResult;
        }
    }
}
