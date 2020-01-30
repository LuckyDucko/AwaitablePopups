using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AwaitablePopupsExample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new AwaitablePopupsExample.FakeLogin.FakeLogin());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
