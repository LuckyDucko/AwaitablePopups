using AwaitablePopups.AbstractClasses;
using System.Windows.Input;
using AwaitablePopups.Interfaces;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using AwaitablePopups.Structs;
using AsyncAwaitBestPractices.MVVM;
using System.Collections.Generic;

namespace AwaitablePopups.PopupPages.Login
{
	public class LoginViewModel : PopupViewModel<(string username, string password)>, ILoginViewModel
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


		public static LoginViewModel GenerateVM()
		{
			return new LoginViewModel(Services.PopupService.GetInstance());
		}

		/// <summary>
		/// provides the LoginPopupPage Generic Type argument to
		/// <see cref="GeneratePopup{TPopupPage}(Dictionary{string, object})"/>
		/// </summary>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<(string username, string password)> GeneratePopup(Dictionary<string, object> propertyDictionary)
		{
			return await GeneratePopup<LoginPopupPage>(propertyDictionary);
		}

		/// <summary>
		/// Attaches properties through the use of reflection. <see cref="PopupViewModel{TReturnable}.InitialiseOptionalProperties(Dictionary{string, object})"/>
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the LoginViewModel ViewModel</typeparam>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<(string username, string password)> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<LoginViewModel>, new()
		{
			InitialiseOptionalProperties(optionalProperties);
			return await Services.PopupService.GetInstance().PushAsync<LoginViewModel, TPopupPage, (string username, string password)>(this);
		}

		/// <summary>
		/// Provides a base dictionary, along with types that you can use to Initialise properties
		/// </summary>
		/// <returns>All Properties contained within the Viewmodel, with their names, current values, and types</returns>
		public virtual Dictionary<string, (object property, Type propertyType)> PullViewModelProperties()
		{
			return base.PullViewModelProperties<LoginViewModel>();
		}

		/// <summary>
		/// provides the LoginPopupPage Generic Type argument to
		/// <see cref="AutoGenerateBasicPopup{TPopupPage}(Color, Color, string, Color, Color, string, Color, string, string, string, string, string, int, int)"/>
		/// </summary>
		public static async Task<(string username, string password)> AutoGenerateBasicPopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string username, string usernamePlaceHolder, string password, string passwordPlaceHolder, string PictureSource, int heightRequest = 0, int widthRequest = 0)
		{
			return await AutoGenerateBasicPopup<LoginPopupPage>(leftButtonColour, leftButtonTextColour, leftButtonText, rightButtonColour, rightButtonTextColour, rightButtonText, mainPopupColour, username, usernamePlaceHolder, password, passwordPlaceHolder, PictureSource, heightRequest, widthRequest);
		}

		public static async Task<(string username, string password)> AutoGenerateBasicPopup<TPopupPage>(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string username, string usernamePlaceHolder, string password, string passwordPlaceHolder, string PictureSource, int heightRequest = 0, int widthRequest = 0) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<LoginViewModel>, new()
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>(("invalid", "invalid")));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>((AutoGeneratePopupViewModel.Username, AutoGeneratePopupViewModel.Password)));

			var propertyDictionary = new Dictionary<string, object>
			{
				{ "LeftButtonCommand", leftButtonCommand },
				{ "LeftButtonColour", leftButtonColour },
				{ "LeftButtonText", leftButtonText ?? "Yes" },
				{ "LeftButtonTextColour", leftButtonTextColour },
				{ "RightButtonCommand", rightButtonCommand },
				{ "RightButtonColour", rightButtonColour },
				{ "RightButtonText", rightButtonText ?? "No" },
				{ "RightButtonTextColour", rightButtonTextColour },
				{ "HeightRequest", heightRequest },
				{ "WidthRequest", widthRequest },
				{ "MainPopupColour", mainPopupColour },
				{ "Username", username},
				{ "UsernamePlaceholder" , usernamePlaceHolder},
				{ "Password" , password},
				{ "PasswordPlaceholder" , passwordPlaceHolder},
				{ "PictureSource" , PictureSource}
			};
			return await AutoGeneratePopupViewModel.GeneratePopup<TPopupPage>(propertyDictionary);
		}



		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, ICommand leftButtonCommand, ICommand rightButtonCommand, string pictureSource, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			PropertySetter(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<LoginViewModel, LoginPopupPage, (string username, string password)>(AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, ICommand leftButtonCommand, ICommand rightButtonCommand, string pictureSource, int heightRequest, int widthRequest, LoginViewModel autoGeneratedLoginViewModel)
		{
			PropertySetter(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, heightRequest, widthRequest, autoGeneratedLoginViewModel);
			return await Services.PopupService.GetInstance().PushAsync<LoginViewModel, LoginPopupPage, (string username, string password)>(autoGeneratedLoginViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, Task<(string username, string password)> leftButtonTask, Task<(string username, string password)> rightButtonTask, string pictureSource, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>(rightButtonTask));
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, string pictureSource, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>(("invalid", "invalid")));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>((AutoGeneratePopupViewModel.Username, AutoGeneratePopupViewModel.Password)));
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<(string username, string password)> GeneratePopup(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, Task leftButtonTask, Task rightButtonTask, string pictureSource, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new LoginViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
			{
				await leftButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>(("invalid", "invalid"));
			});

			AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
			{
				await rightButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<LoginPopupPage>((AutoGeneratePopupViewModel.Username, AutoGeneratePopupViewModel.Password));
			});
			return await GeneratePopup(usernameField, passwordField, leftPopupButton, rightPopupButton, leftButtonCommand, rightButtonCommand, pictureSource, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static void PropertySetter(PopupEntry usernameField, PopupEntry passwordField, PopupButton leftPopupButton, PopupButton rightPopupButton, ICommand leftButtonCommand, ICommand rightButtonCommand, string PictureSource, int heightRequest, int widthRequest, LoginViewModel AutoGeneratePopupViewModel)
		{
			AutoGeneratePopupViewModel.WidthRequest = widthRequest;
			AutoGeneratePopupViewModel.HeightRequest = heightRequest;
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

