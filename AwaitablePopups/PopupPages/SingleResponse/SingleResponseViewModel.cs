using System.Threading.Tasks;
using System.Windows.Input;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using Xamarin.Forms;
using AwaitablePopups.Structs;
using AsyncAwaitBestPractices.MVVM;
using System.Collections.Generic;
using System;
using Rg.Plugins.Popup.Pages;
using System.Reflection;
using System.Linq;

namespace AwaitablePopups.PopupPages.SingleResponse
{
	public class SingleResponseViewModel : PopupViewModel<bool>, ISingleResponseViewModel
	{
		private ICommand _singleButtonCommand;
		public ICommand SingleButtonCommand
		{
			get => _singleButtonCommand;
			set => SetValue(ref _singleButtonCommand, value);
		}

		private string _singleButtonText;
		public string SingleButtonText
		{
			get => _singleButtonText;
			set => SetValue(ref _singleButtonText, value);
		}

		private Color _singleButtonColour;
		public Color SingleButtonColour
		{
			get => _singleButtonColour;
			set => SetValue(ref _singleButtonColour, value);
		}

		private Xamarin.Forms.Color _singleButtonTextColour;
		public Xamarin.Forms.Color SingleButtonTextColour
		{
			get => _singleButtonTextColour;
			set => SetValue(ref _singleButtonTextColour, value);
		}

		private string _singleDisplayImage;
		public string SingleDisplayImage
		{
			get => _singleDisplayImage;
			set => SetValue(ref _singleDisplayImage, value);
		}



		public SingleResponseViewModel(IPopupService popupService) : base(popupService)
		{
		}


		public static SingleResponseViewModel GenerateVM()
		{
			return new SingleResponseViewModel(Services.PopupService.GetInstance());
		}

		/// <summary>
		/// provides the SingleResponsePopupPage Generic Type argument to
		/// <see cref="GeneratePopup{TPopupPage}(Dictionary{string, object})"/>
		/// </summary>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup(Dictionary<string, object> propertyDictionary)
		{
			return await GeneratePopup<SingleResponsePopupPage>(propertyDictionary);
		}

		/// <summary>
		/// Attaches properties through the use of reflection. <see cref="PopupViewModel{TReturnable}.InitialiseOptionalProperties(Dictionary{string, object})"/>
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the SingleResponseViewModel ViewModel</typeparam>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<SingleResponseViewModel>, new()
		{
			InitialiseOptionalProperties(optionalProperties);
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, TPopupPage, bool>(this);
		}


		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup()
		{
			return await GeneratePopup<SingleResponsePopupPage>();
		}

		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup<TPopupPage>() where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<SingleResponseViewModel>, new()
		{
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, TPopupPage, bool>(this);
		}

		public async Task<bool> GeneratePopup(ISingleResponse propertyInterface)
		{
			return await GeneratePopup<SingleResponsePopupPage>(propertyInterface);
		}

		public async Task<bool> GeneratePopup<TPopupPage>(ISingleResponse propertyInterface) where TPopupPage : PopupPage, IGenericViewModel<SingleResponseViewModel>, new()
		{
			PropertyInfo[] properties = typeof(ISingleResponse).GetProperties();
			for (int propertyIndex = 0; propertyIndex < properties.Count(); propertyIndex++)
			{
				GetType().GetProperty(properties[propertyIndex].Name).SetValue(this, properties[propertyIndex].GetValue(propertyInterface, null), null);
			}
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, TPopupPage, bool>(this);
		}




		/// <summary>
		/// Provides a base dictionary, along with types that you can use to Initialise properties
		/// </summary>
		/// <returns>All Properties contained within the Viewmodel, with their names, current values, and types</returns>
		public virtual Dictionary<string, (object property, Type propertyType)> PullViewModelProperties()
		{
			return base.PullViewModelProperties<SingleResponseViewModel>();
		}

		/// <summary>
		/// provides the SingleResponsePopupPage Generic Type argument to
		/// <see cref="AutoGenerateBasicPopup{TPopupPage}(Color, Color, string, Color, string, string, int, int)"/>
		/// <returns> a task awaiting user interaction</returns>
		public static async Task<bool> AutoGenerateBasicPopup(Color buttonColour, Color buttonTextColour, string buttonText, Color mainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await AutoGenerateBasicPopup<SingleResponsePopupPage>(buttonColour, buttonTextColour, buttonText, mainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}

		/// <summary>
		/// Attached basic properties defined.
		/// Left button will return false
		/// Right button will return true
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the SingleResponseViewModel ViewModel</typeparam>
		/// <returns> a task awaiting user interaction</returns>
		public static async Task<bool> AutoGenerateBasicPopup<TPopupPage>(Color buttonColour, Color buttonTextColour, string buttonText, Color mainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<SingleResponseViewModel>, new()
		{
			var AutoGeneratePopupViewModel = new SingleResponseViewModel(Services.PopupService.GetInstance());
			ICommand singleButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(true));

			var propertyDictionary = new Dictionary<string, object>
			{
				{ "SingleButtonCommand", singleButtonCommand },
				{ "SingleButtonColour", buttonColour },
				{ "SingleButtonText", buttonText ?? "Yes" },
				{ "SingleButtonTextColour", buttonTextColour },
				{ "HeightRequest", heightRequest },
				{ "WidthRequest", widthRequest },
				{ "MainPopupInformation", popupInformation ?? "An Error has occured, try again" },
				{ "MainPopupColour", mainPopupColour },
				{ "SingleDisplayImage", displayImageName ?? "NoSource.png" }
			};
			return await AutoGeneratePopupViewModel.GeneratePopup<TPopupPage>(propertyDictionary);
		}






		/// <summary>
		/// designed as the single flowpoint into PropertySetter and providing an awaitable task
		/// </summary>
		/// <param name="buttonColour"></param>
		/// <param name="buttonTextColour"></param>
		/// <param name="buttonText"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		/// <param name="singleButtonCommand"></param>
		/// <param name="AutoGeneratePopupViewModel"></param>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Color MainPopupColour, string popupInformation, string displayImageName, ICommand singleButtonCommand, int heightRequest, int widthRequest, SingleResponseViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, SingleResponsePopupPage, bool>(AutoGeneratePopupViewModel);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static void PropertySetter(Color buttonColour, Color buttonTextColour, string buttonText, Color MainPopupColour, string popupInformation, string displayImageName, ICommand singleButtonCommand, int heightRequest, int widthRequest, SingleResponseViewModel AutoGeneratePopupViewModel)
		{
			AutoGeneratePopupViewModel.WidthRequest = widthRequest;
			AutoGeneratePopupViewModel.HeightRequest = heightRequest;
			AutoGeneratePopupViewModel.SingleButtonCommand = singleButtonCommand;
			AutoGeneratePopupViewModel.SingleButtonColour = buttonColour;
			AutoGeneratePopupViewModel.SingleButtonTextColour = buttonTextColour;
			AutoGeneratePopupViewModel.SingleButtonText = buttonText ?? "Okay";
			AutoGeneratePopupViewModel.MainPopupInformation = popupInformation ?? "An Error has occured, try again";
			AutoGeneratePopupViewModel.MainPopupColour = MainPopupColour;
			AutoGeneratePopupViewModel.SingleDisplayImage = displayImageName ?? "NoSource.png";
		}

		/// <summary>
		/// Unwraps popupbuttons into its parts and reinserts into original function
		/// </summary>
		/// <param name="singleButton"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(PopupButton singleButton, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(singleButton.ButtonColour, singleButton.ButtonTextColour, singleButton.ButtonText, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}

		/// <summary>
		/// Unwraps popupbuttons into its parts and reinserts into original function
		/// Currently UNSTABLE concerning buttonTask, use with care
		/// </summary>
		/// <param name="singleButton"></param>
		/// <param name="buttonTask"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(PopupButton singleButton, Task buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(singleButton.ButtonColour, singleButton.ButtonTextColour, singleButton.ButtonText, buttonTask, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}

		/// <summary>
		/// Unwraps popupbuttons into its parts and reinserts into original function
		/// Currently UNSTABLE concerning buttonTask, use with care
		/// </summary>
		/// <param name="singleButton"></param>
		/// <param name="buttonTask"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(PopupButton singleButton, Task<bool> buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest, int widthRequest)
		{
			return await GeneratePopup(singleButton.ButtonColour, singleButton.ButtonTextColour, singleButton.ButtonText, buttonTask, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest, int widthRequest)
		{
			var AutoGeneratePopupViewModel = new SingleResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			Command singleButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<SingleResponsePopupPage>(true));
			PropertySetter(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, SingleResponsePopupPage, bool>(AutoGeneratePopupViewModel);
		}

		/// <summary>
		/// Currently UNSTABLE concerning buttonTask, use with care
		/// </summary>
		/// <param name="buttonColour"></param>
		/// <param name="buttonTextColour"></param>
		/// <param name="buttonText"></param>
		/// <param name="buttonTask"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Task buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new SingleResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			AsyncCommand singleButtonCommand = new AsyncCommand(async () =>
			{
				await buttonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<SingleResponsePopupPage>(true);
			});
			return await GeneratePopup(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		/// <summary>
		/// Currently UNSTABLE concerning buttonTask, use with care
		/// </summary>
		/// <param name="buttonColour"></param>
		/// <param name="buttonTextColour"></param>
		/// <param name="buttonText"></param>
		/// <param name="buttonTask"></param>
		/// <param name="MainPopupColour"></param>
		/// <param name="popupInformation"></param>
		/// <param name="displayImageName"></param>
		/// <returns></returns>
		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Task<bool> buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new SingleResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			AsyncCommand singleButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<SingleResponsePopupPage>(buttonTask));
			return await GeneratePopup(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
	}
}