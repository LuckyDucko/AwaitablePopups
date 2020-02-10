using AwaitablePopups.Interfaces;
using Rg.Plugins.Popup.Pages;

namespace AwaitablePopups.PopupPages.SingleResponse
{
    public partial class SingleResponsePopupPage : PopupPage, IGenericViewModel<SingleResponseViewModel>
    {
        public SingleResponseViewModel ViewModel
        {
            get => BindingContext as SingleResponseViewModel;
            set => BindingContext = value;
        }

        public void SetViewModel(SingleResponseViewModel viewModel) => ViewModel = viewModel;
        public SingleResponseViewModel GetViewModel() => ViewModel;


        public SingleResponsePopupPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            ViewModel.SafeCloseModal();
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            ViewModel.SafeCloseModal();
            return base.OnBackgroundClicked();
        }

    }
}
