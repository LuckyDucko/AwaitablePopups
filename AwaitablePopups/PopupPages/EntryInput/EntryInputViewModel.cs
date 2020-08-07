using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.PopupPages.TextInput;
using AwaitablePopups.Structs;

using Xamarin.Forms;

namespace AwaitablePopups.PopupPages.EntryInput
{
	public class EntryInputViewModel : PopupViewModel<string>, IEntryInputViewModel
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

		public EntryInputViewModel(IPopupService popupService) : base(popupService)
		{
		}

		public static EntryInputViewModel GenerateVM()
		{
			return new EntryInputViewModel(Services.PopupService.GetInstance());
		}


		/// <summary>
		/// provides the EntryInputPopupPage Generic Type argument to
		/// <see cref="GeneratePopup{TPopupPage}(Dictionary{string, object})"/>
		/// </summary>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup(Dictionary<string, object> propertyDictionary)
		{
			return await GeneratePopup<EntryInputPopupPage>(propertyDictionary);
		}

		/// <summary>
		/// Attaches properties through the use of reflection. <see cref="PopupViewModel{TReturnable}.InitialiseOptionalProperties(Dictionary{string, object})"/>
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the EntryInputViewModel ViewModel</typeparam>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<EntryInputViewModel>, new()
		{
			InitialiseOptionalProperties(optionalProperties);
			return await Services.PopupService.GetInstance().PushAsync<EntryInputViewModel, TPopupPage, string>(this);
		}



		/// <summary>
		/// Provides a base dictionary, along with types that you can use to Initialise properties
		/// </summary>
		/// <returns>All Properties contained within the Viewmodel, with their names, current values, and types</returns>
		public virtual Dictionary<string, (object property, Type propertyType)> PullViewModelProperties()
		{
			return base.PullViewModelProperties<EntryInputViewModel>();
		}

		/// <summary>
		/// provides the EntryInputPopupPage Generic Type argument to
		/// <see cref="AutoGenerateBasicPopup{TPopupPage}(Color, Color, string, Color, Color, string, Color, string, string, int, int)"/>
		/// </summary>
		public static async Task<string> AutoGenerateBasicPopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await AutoGenerateBasicPopup<EntryInputPopupPage>(leftButtonColour, leftButtonTextColour, leftButtonText, rightButtonColour, rightButtonTextColour, rightButtonText, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}

		public static async Task<string> AutoGenerateBasicPopup<TPopupPage>(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<EntryInputViewModel>, new()
		{
			var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>("No Text Available"));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(AutoGeneratePopupViewModel.TextInput));

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
				{ "PlaceHolderInput", defaultPlaceHolder},
				{ "MainPopupColour", mainPopupColour },
				{ "TextInput", defaultTextInput }
			};
			return await AutoGeneratePopupViewModel.GeneratePopup(propertyDictionary);
		}





		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static async Task<string> GeneratePopup(Color leftButtonColour,
												  Color leftButtonTextColour,
												  string leftButtonText,
												  ICommand leftButtonCommand,
												  Color rightButtonColour,
												  Color rightButtonTextColour,
												  string rightButtonText,
												  ICommand rightButtonCommand,
												  Color mainPopupColour,
												  string defaultTextInput,
												  string defaultPlaceHolder,
												  int heightRequest,
												  int widthRequest,
												  EntryInputViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<EntryInputViewModel, EntryInputPopupPage, string>(AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task<bool> leftButtonTask, Task<bool> rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task leftButtonTask, Task rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task<string> leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task<string> rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(rightButtonTask));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>("No Text Available"));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(AutoGeneratePopupViewModel.TextInput));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task rightButtonTask, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
			AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
			{
				await leftButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(string.Empty);
			});

			AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
			{
				await rightButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(AutoGeneratePopupViewModel.TextInput);
			});
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static void PropertySetter(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest, int widthRequest, EntryInputViewModel AutoGeneratePopupViewModel)
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

