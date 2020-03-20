using AwaitablePopups.Interfaces;
using Rg.Plugins.Popup.Pages;
namespace AwaitablePopups.PopupPages.TextInput
{
    public partial class TextInputPopupPage : PopupPage, IGenericViewModel<TextInputViewModel>
    {
        public TextInputViewModel ViewModel
        {
            get => BindingContext as TextInputViewModel;
            set => BindingContext = value;
        }


        public TextInputPopupPage()
        {
            InitializeComponent();
        }

        public TextInputViewModel GetViewModel() => ViewModel;
        public void SetViewModel(TextInputViewModel viewModel) => ViewModel = viewModel;


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        protected override bool OnBackButtonPressed()
        {
            ViewModel.SafeCloseModal();
            return base.OnBackButtonPressed();
        }

    }
}
