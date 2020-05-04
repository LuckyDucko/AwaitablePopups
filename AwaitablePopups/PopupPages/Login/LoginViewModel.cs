using AwaitablePopups.AbstractClasses;
using System.Windows.Input;
using AwaitablePopups.Interfaces;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using AwaitablePopups.Structs;
using System.Collections.Generic;
using AsyncAwaitBestPractices.MVVM;

namespace AwaitablePopups.PopupPages.Login
{
	public class LoginViewModel : PopupViewModel<(string username, string password)>
	{
		private string _username;
		public string Username
		{
			get => _username;
			set => SetValue(ref _username, value);
		}

		private string _usernamePlaceholder;
		public string UsernamePlaceholder
		{
			get => _usernamePlaceholder;
			set => SetValue(ref _usernamePlaceholder, value);
		}

		private Xamarin.Forms.Color _usernamePlaceholderColour; //Ensure this is _name
		public Xamarin.Forms.Color UsernamePlaceholderColour //Ensure this is Name
		{
			get => _usernamePlaceholderColour;
			set => SetValue(ref _usernamePlaceholderColour, value);
		}

		private Xamarin.Forms.Color _usernameTextColour; //Ensure this is _name
		public Xamarin.Forms.Color UsernameTextColour //Ensure this is Name
		{
			get => _usernameTextColour;
			set => SetValue(ref _usernameTextColour, value);
		}

		private Xamarin.Forms.Color _usernameBackgroundColour; //Ensure this is _name
		public Xamarin.Forms.Color UsernameBackgroundColour //Ensure this is Name
		{
			get => _usernameBackgroundColour;
			set => SetValue(ref _usernameBackgroundColour, value);
		}

		private string _password;
		public string Password
		{
			get => _password;
			set => SetValue(ref _password, value);
		}

		private string _passwordPlaceholder;
		public string PasswordPlaceholder
		{
			get => _passwordPlaceholder;
			set => SetValue(ref _passwordPlaceholder, value);
		}

		private Xamarin.Forms.Color _passwordPlaceholderColour; //Ensure this is _name
		public Xamarin.Forms.Color PasswordPlaceholderColour //Ensure this is Name
		{
			get => _passwordPlaceholderColour;
			set => SetValue(ref _passwordPlaceholderColour, value);
		}

		private Xamarin.Forms.Color _passwordTextColour; //Ensure this is _name
		public Xamarin.Forms.Color PasswordTextColour //Ensure this is Name
		{
			get => _passwordTextColour;
			set => SetValue(ref _passwordTextColour, value);
		}

		private Xamarin.Forms.Color _passwordBackgroundColour; //Ensure this is _name
		public Xamarin.Forms.Color PasswordBackgroundColour //Ensure this is Name
		{
			get => _passwordBackgroundColour;
			set => SetValue(ref _passwordBackgroundColour, value);
		}

		private ICommand _leftButtonCommand;
		public ICommand LeftButtonCommand
		{
			get => _leftButtonCommand;
			set => SetValue(ref _leftButtonCommand, value);
		}

		private string _leftButtonText;
		public string LeftButtonText
		{
			get => _leftButtonText;
			set => SetValue(ref _leftButtonText, value);
		}

		private Xamarin.Forms.Color _leftButtonColour;
		public Xamarin.Forms.Color LeftButtonColour
		{
			get => _leftButtonColour;
			set => SetValue(ref _leftButtonColour, value);
		}

		private Xamarin.Forms.Color _leftButtonTextColour;
		public Xamarin.Forms.Color LeftButtonTextColour
		{
			get => _leftButtonTextColour;
			set => SetValue(ref _leftButtonTextColour, value);
		}

		private ICommand _rightButtonCommand;
		public ICommand RightButtonCommand
		{
			get => _rightButtonCommand;
			set => SetValue(ref _rightButtonCommand, value);
		}

		private string _rightButtonText;
		public string RightButtonText
		{
			get => _rightButtonText;
			set => SetValue(ref _rightButtonText, value);
		}

		private string _pictureSource;
		public string PictureSource
		{
			get => _pictureSource;
			set => SetValue(ref _pictureSource, value);
		}

		private Xamarin.Forms.Color _rightButtonColour;
		public Xamarin.Forms.Color RightButtonColour
		{
			get => _rightButtonColour;
			set => SetValue(ref _rightButtonColour, value);
		}

		private Xamarin.Forms.Color _rightButtonTextColour;
		public Xamarin.Forms.Color RightButtonTextColour
		{
			get => _rightButtonTextColour;
			set => SetValue(ref _rightButtonTextColour, value);
		}

		public LoginViewModel(IPopupService popupService) : base(popupService)
		{
		}

		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, ICommand leftButtonCommand, ICommand rightButtonCommand, string pictureSource)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			PropertySetter(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<LoginViewModel, LoginPopupPage, (string username, string password)>(AutoGeneratePopupViewModel);
		}

		/// <summary>
		/// Only use if you know what you are doing. untested.
		/// </summary>
		/// <param name="keyValueProperties"></param>
		/// <returns></returns>
		public static async Task<(string username, string password)> GeneratePopup(Dictionary<string, object> keyValueProperties)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(Services.PopupService.GetInstance());
			AutoGeneratePopupViewModel.InitialiseOptionalProperties(keyValueProperties);
			return await Services.PopupService.GetInstance().PushAsync<LoginViewModel, LoginPopupPage, (string username, string password)>(AutoGeneratePopupViewModel);
		}

		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, Task<(string username, string password)> leftButtonTask, Task<(string username, string password)> rightButtonTask, string pictureSource)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal(rightButtonTask));
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource);
		}

		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, string pictureSource)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal(("invalid", "invalid")));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal((AutoGeneratePopupViewModel.Username, AutoGeneratePopupViewModel.Password)));
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource);
		}

		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, Task leftButtonTask, Task rightButtonTask, string pictureSource)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
			{
				await leftButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal(("invalid", "invalid"));
			});

			AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
			{
				await rightButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal((AutoGeneratePopupViewModel.Username, AutoGeneratePopupViewModel.Password));
			});
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource);
		}

		private static void PropertySetter(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, ICommand leftButtonCommand, ICommand rightButtonCommand, string PictureSource, LoginViewModel AutoGeneratePopupViewModel)
		{
			AutoGeneratePopupViewModel.Username = usernameField.EntryText;
			AutoGeneratePopupViewModel.UsernamePlaceholder = usernameField.EntryPlaceholder;
			AutoGeneratePopupViewModel.UsernamePlaceholderColour = usernameField.PlaceholderTextColour;
			AutoGeneratePopupViewModel.UsernameTextColour = usernameField.EntryTextColour;
			AutoGeneratePopupViewModel.UsernameBackgroundColour = usernameField.BackgroundColour;
			AutoGeneratePopupViewModel.Password = passwordField.EntryText;
			AutoGeneratePopupViewModel.PasswordPlaceholder = passwordField.EntryPlaceholder;
			AutoGeneratePopupViewModel.PasswordPlaceholderColour = passwordField.PlaceholderTextColour;
			AutoGeneratePopupViewModel.PasswordTextColour = passwordField.EntryTextColour;
			AutoGeneratePopupViewModel.PasswordBackgroundColour = passwordField.BackgroundColour;
			AutoGeneratePopupViewModel.LeftButtonCommand = leftButtonCommand ?? throw new ArgumentNullException(nameof(leftButtonCommand));
			AutoGeneratePopupViewModel.LeftButtonText = leftPopupButton.ButtonText;
			AutoGeneratePopupViewModel.LeftButtonColour = leftPopupButton.ButtonColour;
			AutoGeneratePopupViewModel.LeftButtonTextColour = leftPopupButton.ButtonTextColour;
			AutoGeneratePopupViewModel.RightButtonCommand = rightButtonCommand ?? throw new ArgumentNullException(nameof(rightButtonCommand));
			AutoGeneratePopupViewModel.RightButtonText = rightPopupButton.ButtonText;
			AutoGeneratePopupViewModel.RightButtonColour = rightPopupButton.ButtonColour;
			AutoGeneratePopupViewModel.RightButtonTextColour = rightPopupButton.ButtonTextColour;
			AutoGeneratePopupViewModel.PictureSource = PictureSource;
		}
	}
}

