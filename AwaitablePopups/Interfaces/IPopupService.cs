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
        Task<TAsyncActionResult> WrapReturnableTaskInLoader<TAsyncActionResult>(Task<TAsyncActionResult> action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor);
        Task WrapTaskInLoader(Task action, Color loaderColour, Color loaderPopupColour, List<string> reasonsForLoader, Color textColor);

        TPopupPage CreatePopupPage<TPopupPage>()
            where TPopupPage : PopupPage, new();
        TPopupPage AttachViewModel<TPopupPage, TViewModel>(TPopupPage popupPage, TViewModel viewModel)
            where TPopupPage : PopupPage, IGenericViewModel<TViewModel>
            where TViewModel : BasePopupViewModel;
        Task<TReturnable> PushAsync<TViewModel, TPopupPage, TReturnable>(TViewModel modalViewModel)
            where TPopupPage : PopupPage, IGenericViewModel<TViewModel>, new()
            where TViewModel : PopupViewModel<TReturnable>;
        void PopAsync();
    }
}
