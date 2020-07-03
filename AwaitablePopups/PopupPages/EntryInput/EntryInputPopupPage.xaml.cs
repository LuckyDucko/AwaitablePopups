using AwaitablePopups.Interfaces;
using AwaitablePopups.PopupPages.EntryInput;

using Rg.Plugins.Popup.Pages;
namespace AwaitablePopups.PopupPages.EntryInput
{
    public partial class EntryInputPopupPage : PopupPage, IGenericViewModel<EntryInputViewModel>
    {
        public EntryInputViewModel ViewModel
        {
            get => BindingContext as EntryInputViewModel;
            set => BindingContext = value;
        }


        public EntryInputPopupPage()
        {
            InitializeComponent();
        }

        public EntryInputViewModel GetViewModel() => ViewModel;
        public void SetViewModel(EntryInputViewModel viewModel) => ViewModel = viewModel;


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        protected override bool OnBackButtonPressed()
        {
            ViewModel.SafeCloseModal<EntryInputPopupPage>();
            return base.OnBackButtonPressed();
        }

    }
}
