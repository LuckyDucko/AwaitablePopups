using System;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.Structs;

using Xamarin.Forms;

namespace AwaitablePopups.PopupPages.TextInput
{
	public class TextInputViewModel : PopupViewModel<string>, ITextInputViewModel
	{
		private string _textInput;
		public string TextInput
		{
			get => _textInput;
			set => SetValue(ref _textInput, value);
		}

		private string _placeHolderInput;
		public string PlaceHolderInput
		{
			get => _placeHolderInput;
			set => SetValue(ref _placeHolderInput, value);
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

		public TextInputViewModel(IPopupService popupService) : base(popupService)
		{
		}

		private static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest, int widthRequest, TextInputViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<TextInputViewModel, TextInputPopupPage, string>(AutoGeneratePopupViewModel);
		}

		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task<bool> leftButtonTask, Task<bool> rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}

		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}

		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task leftButtonTask, Task rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}

		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task<string> leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task<string> rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(rightButtonTask));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>("No Text Available"));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(AutoGeneratePopupViewModel.TextInput));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
			{
				await leftButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(string.Empty);
			});

			AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
			{
				await rightButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(AutoGeneratePopupViewModel.TextInput);
			});
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}


		private static void PropertySetter(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest, int widthRequest, TextInputViewModel AutoGeneratePopupViewModel)
		{
			AutoGeneratePopupViewModel.LeftButtonCommand = leftButtonCommand;
			AutoGeneratePopupViewModel.LeftButtonColour = leftButtonColour;
			AutoGeneratePopupViewModel.LeftButtonText = leftButtonText ?? "No";
			AutoGeneratePopupViewModel.LeftButtonTextColour = leftButtonTextColour;

			AutoGeneratePopupViewModel.RightButtonCommand = rightButtonCommand;
			AutoGeneratePopupViewModel.RightButtonColour = rightButtonColour;
			AutoGeneratePopupViewModel.RightButtonText = rightButtonText ?? "Yes";
			AutoGeneratePopupViewModel.RightButtonTextColour = rightButtonTextColour;

			AutoGeneratePopupViewModel.HeightRequest = heightRequest;
			AutoGeneratePopupViewModel.WidthRequest = widthRequest;
			AutoGeneratePopupViewModel.MainPopupColour = mainPopupColour;
			AutoGeneratePopupViewModel.TextInput = defaultTextInput;
			AutoGeneratePopupViewModel.PlaceHolderInput = defaultPlaceHolder;
		}
	}
}

