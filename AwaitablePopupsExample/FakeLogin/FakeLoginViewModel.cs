using System;
using System.Threading.Tasks;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.PopupPages.DualResponse;
using AwaitablePopups.PopupPages.SingleResponse;
using AsyncAwaitBestPractices.MVVM;
using AwaitablePopups.Structs;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading;
using AwaitablePopups.PopupPages.TextInput;

namespace AwaitablePopupsExample.FakeLogin
{
    public class FakeLoginViewModel : BasePopupViewModel
    {

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set => SetValue(ref _mobile, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetValue(ref _password, value);
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set => SetValue(ref _rememberMe, value);
        }

        private bool _loginEnabled = true;
        public bool LoginEnabled
        {
            get => _loginEnabled;
            set => SetValue(ref _loginEnabled, value);
        }


        public IAsyncCommand LoginAsync => new AsyncCommand(AttemptLogin);

        public FakeLoginViewModel(IPopupService popupService) : base(popupService)
        {
        }

        private bool LongRunningFunction(int MillisecondDelay)
        {
            Thread.Sleep(6000);
            return true;
        }

        public async Task<bool> AttemptLogin()
        {
            LoginEnabled = false;
            var textinput = await TextInputViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Green, "TEXT HERE", "Placeholder");
            var loginResult = await PopupService.WrapReturnableFuncInLoader(LongRunningFunction, 6000, Color.Blue, Color.White, LoadingReasons(), Color.Black);
            try
            {
                if (string.IsNullOrEmpty(Mobile) || string.IsNullOrEmpty(Password))
                {
                    loginResult = await IncorrectLoginAsync();
                }
                else if (Mobile.Equals("Dual"))
                {
                    loginResult = await AcceptConditions();
                }
                else if (Mobile.Equals("Single"))
                {
                    loginResult = await SuccessfulLoginAsync();
                }
            }
            catch (Exception)
            {
                loginResult = await GenericErrorAsync();
            }
            finally
            {
                RememberMe = loginResult;
                Mobile = loginResult ? "Returned a true" : "Returned a false";
                LoginEnabled = true;
            }
            return loginResult;
        }

        private static List<string> LoadingReasons()
        {
            return new List<string>
            {
                "Twiddling Thumbs",
                "Rolling Eyes",
                "Checking Watch",
                "General Complaining",
                "Calling in late to work",
                "Waiting"
            };
        }


        private async Task<bool> GenericErrorAsync()
        {
            await PopupService.WrapTaskInLoader(Task.Delay(10000), Color.Blue, Color.White, LoadingReasons(), Color.Black);
            return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.Coral, Color.Black, "Okay"), Color.Gray, "There was a Device error. Dang.", "NoSource.png");
        }


        private async Task<bool> IncorrectLoginAsync()
        {
            await PopupService.WrapTaskInLoader(Task.Delay(10000), Color.Blue, Color.White, LoadingReasons(), Color.Black);
            return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.Goldenrod, Color.Black, "Okay"), Color.Gray, "Your Phone Number or Pin is incorrect, please try again.", "NoSource.png");
        }

        private async Task<bool> SuccessfulLoginAsync()
        {
            await PopupService.WrapTaskInLoader(Task.Delay(10000), Color.Blue, Color.White, LoadingReasons(), Color.Black);
            return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.HotPink, Color.Black, "I Accept"), Color.Gray, "Good Job.", "NoSource.png");
        }

        private async Task<bool> AcceptConditions()
        {
            await PopupService.WrapTaskInLoader(Task.Delay(10000), Color.Blue, Color.White, LoadingReasons(), Color.Black);
            return await DualResponseViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Gray, "Do you accept the terms and conditions?", "NoSource.png");
        }
    }
}
