﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.Structs;

using Rg.Plugins.Popup.Pages;

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

		public static TextInputViewModel GenerateVM()
		{
			return new TextInputViewModel(Services.PopupService.GetInstance());
		}

		/// <summary>
		/// provides the TextInputPopupPage Generic Type argument to
		/// <see cref="GeneratePopup{TPopupPage}(Dictionary{string, object})"/>
		/// </summary>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup(Dictionary<string, object> propertyDictionary)
		{
			return await GeneratePopup<TextInputPopupPage>(propertyDictionary);
		}

		/// <summary>
		/// Attaches properties through the use of reflection. <see cref="PopupViewModel{TReturnable}.InitialiseOptionalProperties(Dictionary{string, object})"/>
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the TextInputViewModel ViewModel</typeparam>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<TextInputViewModel>, new()
		{
			InitialiseOptionalProperties(optionalProperties);
			return await Services.PopupService.GetInstance().PushAsync<TextInputViewModel, TPopupPage, string>(this);
		}

		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup()
		{
			return await GeneratePopup<TextInputPopupPage>();
		}

		/// <typeparam name="TPopupPage">User defined page that uses the TextInputViewModel ViewModel</typeparam>
		/// <returns> Task that waits for user input</returns>
		public async Task<string> GeneratePopup<TPopupPage>() where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<TextInputViewModel>, new()
		{
			return await Services.PopupService.GetInstance().PushAsync<TextInputViewModel, TPopupPage, string>(this);
		}

		public async Task<string> GeneratePopup(ITextInput propertyInterface)
		{
			return await GeneratePopup<TextInputPopupPage>(propertyInterface);
		}

		public async Task<string> GeneratePopup<TPopupPage>(ITextInput propertyInterface) where TPopupPage : PopupPage, IGenericViewModel<TextInputViewModel>, new()
		{
			PropertyInfo[] properties = typeof(ITextInput).GetProperties();
			for (int propertyIndex = 0; propertyIndex < properties.Count(); propertyIndex++)
			{
				GetType().GetProperty(properties[propertyIndex].Name).SetValue(this, properties[propertyIndex].GetValue(propertyInterface, null), null);
			}
			return await Services.PopupService.GetInstance().PushAsync<TextInputViewModel, TPopupPage, string>(this);
		}

		/// <summary>
		/// Provides a base dictionary, along with types that you can use to Initialise properties
		/// </summary>
		/// <returns>All Properties contained within the Viewmodel, with their names, current values, and types</returns>
		public virtual Dictionary<string, (object property, Type propertyType)> PullViewModelProperties()
		{
			return base.PullViewModelProperties<TextInputViewModel>();
		}

		/// <summary>
		/// provides the TextInputPopupPage Generic Type argument to
		/// <see cref="AutoGenerateBasicPopup{TPopupPage}(Color, Color, string, Color, Color, string, Color, string, string, int, int)"/>
		/// </summary>
		public static async Task<string> AutoGenerateBasicPopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			return await AutoGenerateBasicPopup<TextInputPopupPage>(leftButtonColour, leftButtonTextColour, leftButtonText, rightButtonColour, rightButtonTextColour, rightButtonText, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest);
		}

		public static async Task<string> AutoGenerateBasicPopup<TPopupPage>(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<TextInputViewModel>, new()
		{
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TPopupPage>("No Text Available"));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(AutoGeneratePopupViewModel.TextInput));

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
			return await AutoGeneratePopupViewModel.GeneratePopup<TPopupPage>(propertyDictionary);
		}


		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest, int widthRequest, TextInputViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<TextInputViewModel, TextInputPopupPage, string>(AutoGeneratePopupViewModel);
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
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(rightButtonTask));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string defaultTextInput, string defaultPlaceHolder, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new TextInputViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>("No Text Available"));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TextInputPopupPage>(AutoGeneratePopupViewModel.TextInput));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, mainPopupColour, defaultTextInput, defaultPlaceHolder, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
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

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
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

