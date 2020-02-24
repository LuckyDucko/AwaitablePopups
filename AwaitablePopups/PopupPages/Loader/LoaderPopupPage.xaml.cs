using System;
using System.Collections.Generic;
using AwaitablePopups.Interfaces;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace AwaitablePopups.PopupPages.Loader
{
    public partial class LoaderPopupPage : PopupPage, IGenericViewModel<LoaderViewModel>
    {
        public LoaderPopupPage()
        {
            InitializeComponent();
        }

        public LoaderViewModel ViewModel
        {
            get => BindingContext as LoaderViewModel;
            set => BindingContext = value;
        }

        public void SetViewModel(LoaderViewModel viewModel) => ViewModel = viewModel;
        public LoaderViewModel GetViewModel() => ViewModel;

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
