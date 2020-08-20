using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AwaitablePopups.AbstractClasses;

using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
	public interface IPopupService
	{
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TSyncActionResult>(Func<TSyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TSyncActionResult>(Func<TSyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TSyncActionResult>(Func<TArgument1, TSyncActionResult> action, TArgument1 argument1, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TSyncActionResult>(Func<TArgument1, TSyncActionResult> action, TArgument1 argument1, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TSyncActionResult>(Func<TArgument1, TArgument2, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TSyncActionResult>(Func<TArgument1, TArgument2, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TSyncActionResult> WrapReturnableFuncInLoader<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult>(Func<TArgument1, TArgument2, TArgument3, TArgument4, TArgument5, TArgument6, TSyncActionResult> action, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3, TArgument4 argument4, TArgument5 argument5, TArgument6 argument6, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task<TAsyncActionResult> WrapReturnableTaskInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor);
		Task<TAsyncActionResult> WrapReturnableTaskInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor, int millisecondsBetweenReasons);

		Task<TAsyncActionResult> WrapReturnableTCSInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour);
		Task<TAsyncActionResult> WrapReturnableTCSInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColour, int millisecondsBetweenReasons);

		Task WrapTaskInLoader(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor);
		Task WrapTaskInLoader(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor, int millisecondsBetweenReasons);

		TPopupPage CreatePopupPage<TPopupPage>()
			where TPopupPage : PopupPage, new();
		TPopupPage AttachViewModel<TPopupPage, TViewModel>(TPopupPage popupPage, TViewModel viewModel)
			where TPopupPage : PopupPage, IGenericViewModel<TViewModel>
			where TViewModel : BasePopupViewModel;

		/// <typeparam name="TViewModel">The ViewModel Type that is Attached to the Generic PopupPage</typeparam>
		/// <typeparam name="TPopupPage">The Generic Popupup that will be pushed onto the Navigation Stack</typeparam>
		/// <typeparam name="TReturnable">The Type that we expect to be return from the PopupPage through the generic ViewModel </typeparam>
		/// <param name="modalViewModel">The ViewModel that has been created</param>
		/// <returns>an Async Task that will wait for the PopupPage to return its value through its Viewmodel</returns>
		Task<TReturnable> PushAsync<TViewModel, TPopupPage, TReturnable>(TViewModel modalViewModel)
			where TPopupPage : PopupPage, IGenericViewModel<TViewModel>, new()
			where TViewModel : PopupViewModel<TReturnable>;

		void PopAsync<TPopupType>() where TPopupType : PopupPage, new();

		/// <summary>
		/// Added the ability to specify an action when an exception happens when popping.
		/// An example of this is issues on detach, such as AiForms.Effects on popupPages
		/// </summary>
		/// <typeparam name="TPopupType"></typeparam>
		/// <param name="exceptionActionForSafeFireAndForget"></param>
		void PopAsync<TPopupType>(Action<Exception> exceptionActionForSafeFireAndForget) where TPopupType : PopupPage, new();

	}
}