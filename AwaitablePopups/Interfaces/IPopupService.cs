using System;
using System.Threading.Tasks;
using AwaitablePopups.AbstractClasses;
using Rg.Plugins.Popup.Pages;

namespace AwaitablePopups.Interfaces
{
    public interface IPopupService
    {
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
