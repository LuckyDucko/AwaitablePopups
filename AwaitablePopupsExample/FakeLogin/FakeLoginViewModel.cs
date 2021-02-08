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
using AwaitablePopups.PopupPages.EntryInput;
using AwaitablePopups.PopupPages.Login;
using System.Windows.Input;
using AwaitablePopups.PopupPages.Loader;

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
			try
			{
				await EmptyCredentialCheck();
				LoginEnabled = false;


				await EntryInputViewModel.AutoGenerateBasicPopup(Color.WhiteSmoke, Color.Red, "Cancel", Color.WhiteSmoke, Color.Green, "Submit", Color.DimGray, "Text input Example", string.Empty);
				var dualresponseinput = await DualResponseViewModel.AutoGenerateBasicPopup(Color.WhiteSmoke, Color.Red, "Okay", Color.WhiteSmoke, Color.Green, "Looks Good!", Color.DimGray, "This is an example of a dual response popup page", "thumbsup.png");
				dualresponseinput = dualresponseinput == await PopupService.WrapReturnableFuncInLoader(LongRunningFunction, 6000, dualresponseinput, Color.Red, Color.White, LoadingReasons(), Color.Black);
				await PopupService.WrapReturnableTaskInLoader<bool, LoaderPopupPage>(NeedlessSpinnerOfAFunction(1000, true), Color.Blue, Color.White, LoadingReasons(), Color.Black);
				return await NeedlessTestAbstraction(dualresponseinput);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		private async Task EmptyCredentialCheck()
		{
			if (string.IsNullOrEmpty(Mobile) || string.IsNullOrEmpty(Password))
			{
				var (username, password) = await LoginViewModel.AutoGenerateBasicPopup(Color.WhiteSmoke, Color.Red, "Cancel", Color.WhiteSmoke, Color.Green, "Submit", Color.DimGray, string.Empty, "Username Here", string.Empty, "Password here", "thumbsup.png", 0, 0);
				Mobile = username;
				Password = password;
			}
		}


		private async Task<bool> NeedlessSpinnerOfAFunction(int MillisecondDelay, bool pointlessBoolean)
		{
			await Task.Delay(MillisecondDelay);
			return pointlessBoolean;
		}

		private async Task<bool> NeedlessTestAbstraction(bool loginResult)
		{
			try
			{
				if (Mobile.Equals("Dual"))
				{
					loginResult = await AcceptConditions();
				}
				else
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

		/// <summary>
		/// this is an example of why you would use this method. to reproduce based on an existing VM
		/// this is a far fetched example i'll admit.
		/// </summary>
		/// <param name="VM"></param>
		/// <returns></returns>
		private async Task<bool> ExampleOfDictionaryUsingPullProperties(SingleResponseViewModel VM)
		{
			var VMDictionary = VM.PullViewModelProperties();
			VMDictionary["SingleButtonCommand"] = (new AsyncCommand(async () =>
			{
				await PopupService.WrapReturnableFuncInLoader(LongRunningFunction, 4000, true, Color.Green, Color.White, LoadingReasons(), Color.Black);
				var innervm = SingleResponseViewModel.GenerateVM();
				var innervmdictionary = VM.PullViewModelProperties();
				innervmdictionary["SingleButtonCommand"] = (new Command(() => innervm.SafeCloseModal<SingleResponsePopupPage>(true)), typeof(Command));
				VM.SafeCloseModal<SingleResponsePopupPage>(await innervm.GeneratePopup(innervm.FinalisePreparedProperties(innervmdictionary)));
			}), typeof(AsyncCommand));
			VMDictionary["SingleButtonColour"] = (Color.HotPink, typeof(Color));
			VMDictionary["SingleButtonText"] = ("Nice", typeof(string));
			VMDictionary["SingleButtonTextColour"] = (Color.Black, typeof(Color));
			VMDictionary["MainPopupInformation"] = ("Good Job, enjoy this single response example", typeof(string));
			VMDictionary["MainPopupColour"] = (Color.DimGray, typeof(Color));
			VMDictionary["SingleDisplayImage"] = ("thumbsup.png", typeof(string));
			return await VM.GeneratePopup(VM.FinalisePreparedProperties(VMDictionary));
		}

		private async Task<bool> GenericErrorAsync()
		{

			return await SingleResponseViewModel.AutoGenerateBasicPopup(Color.Coral, Color.Black, "Okay", Color.Gray, "There was a Device error. Dang. But the SingleResponse page example looks nice", "thumbsup.png");
		}

		private async Task<bool> SuccessfulLoginAsync()
		{
			return await SingleResponseViewModel.AutoGenerateBasicPopup(Color.HotPink, Color.Black, "I Accept", Color.Gray, "Good Job, enjoy this single response example", "thumbsup.png");
		}

		private async Task<bool> AcceptConditions()
		{
			return await ExampleOfDictionaryUsingPullProperties(SingleResponseViewModel.GenerateVM());
		}
	}
}
