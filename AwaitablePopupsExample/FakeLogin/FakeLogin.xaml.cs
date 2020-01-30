using System;
using System.Collections.Generic;
using Awaitable.Services;
using Xamarin.Forms;

namespace AwaitablePopupsExample.FakeLogin
{
    public partial class FakeLogin : ContentPage
    {
        public FakeLoginViewModel ViewModel
        {
            get => BindingContext as FakeLoginViewModel;
            set => BindingContext = value;
        }

        public FakeLogin()
        {
            InitializeComponent();
            var popupService = PopupService.GetInstance();
            ViewModel = new FakeLoginViewModel(popupService);
        }
    }
}
