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
using AsyncAwaitBestPractices;

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

		private bool LongRunningFunction(int MillisecondDelay, bool pointlessBoolean)
		{
			Thread.Sleep(6000);
			return pointlessBoolean;
		}

		public async Task<bool> AttemptLogin()
		{
			LoginEnabled = false;
			var textinput = await TextInputViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Green, "TEXT HERE", "Placeholder");
			var pointlessBoolean = await DualResponseViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Gray, string.Concat("Your Text:", textinput, "I am doing this weird formatting to attempt to break this with weird scenarios"), "NoSource.png");
			pointlessBoolean = pointlessBoolean == await PopupService.WrapReturnableFuncInLoader(LongRunningFunction, 6000, pointlessBoolean, Color.Red, Color.White, LoadingReasons(), Color.Black);
			return await PopupService.WrapReturnableTaskInLoader(NeedlessTestAbstraction(pointlessBoolean), Color.Blue, Color.White, LoadingReasons(), Color.Black);
		}

		private async Task<bool> NeedlessTestAbstraction(bool loginResult)
		{
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

			return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.Coral, Color.Black, "Okay"), Color.Gray, "There was a Device error. Dang.", "NoSource.png");
		}


		private async Task<bool> IncorrectLoginAsync()
		{
			return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.Goldenrod, Color.Black, "Okay"), Color.Gray, "Your Phone Number or Pin is incorrect, please try again.", "NoSource.png");
		}

		private async Task<bool> SuccessfulLoginAsync()
		{
			return await SingleResponseViewModel.GeneratePopup(new PopupButton(Color.HotPink, Color.Black, "I Accept"), Color.Gray, "Good Job.", "NoSource.png");
		}

		private async Task<bool> AcceptConditions()
		{
			return await DualResponseViewModel.GeneratePopup(new PopupButton(Color.Green, Color.Black, "I Accept"), new PopupButton(Color.Red, Color.Black, "I decline"), Color.Gray, "Do you accept the terms and conditions?", "NoSource.png");
		}
	}
}
