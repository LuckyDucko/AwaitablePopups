using AwaitablePopups.AbstractClasses;

namespace AwaitablePopups.Interfaces
{
    public interface IGenericViewModel<TViewModel> where TViewModel : BasePopupViewModel
    {
        void SetViewModel(TViewModel viewModel);
        TViewModel GetViewModel();
    }
}