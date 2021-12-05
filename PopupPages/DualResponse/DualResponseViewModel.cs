using System;
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

namespace AwaitablePopups.PopupPages.DualResponse
{
	public class DualResponseViewModel : PopupViewModel<bool>, IDualResponseViewModel
	{
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

		public DualResponseViewModel(IPopupService popupService) : base(popupService)
		{
		}

		public static DualResponseViewModel GenerateVM()
		{
			return new DualResponseViewModel(Services.PopupService.GetInstance());
		}

		/// <summary>
		/// provides the DualResponsePopupPage Generic Type argument to
		/// <see cref="GeneratePopup{TPopupPage}(Dictionary{string, object})"/>
		/// </summary>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup(Dictionary<string, object> propertyDictionary)
		{
			return await GeneratePopup<DualResponsePopupPage>(propertyDictionary);
		}

		/// <summary>
		/// Attaches properties through the use of reflection. <see cref="PopupViewModel{TReturnable}.InitialiseOptionalProperties(Dictionary{string, object})"/>
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the DualResponseViewModel ViewModel</typeparam>
		/// <param name="propertyDictionary">Page Properties, for an example pull <seealso cref="PullViewModelProperties"/></param>
		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup<TPopupPage>(Dictionary<string, object> optionalProperties) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<DualResponseViewModel>, new()
		{
			InitialiseOptionalProperties(optionalProperties);
			return await Services.PopupService.GetInstance().PushAsync<DualResponseViewModel, TPopupPage, bool>(this);
		}

		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup()
		{
			return await GeneratePopup<DualResponsePopupPage>();
		}

		/// <returns> Task that waits for user input</returns>
		public async Task<bool> GeneratePopup<TPopupPage>() where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<DualResponseViewModel>, new()
		{
			return await Services.PopupService.GetInstance().PushAsync<DualResponseViewModel, TPopupPage, bool>(this);
		}

		public async Task<bool> GeneratePopup(IDualResponse propertyInterface)
		{
			return await GeneratePopup<DualResponsePopupPage>(propertyInterface);
		}

		public async Task<bool> GeneratePopup<TPopupPage>(IDualResponse propertyInterface) where TPopupPage : PopupPage, IGenericViewModel<DualResponseViewModel>, new()
		{
			PropertyInfo[] properties = typeof(IDualResponse).GetProperties();
			for (int propertyIndex = 0; propertyIndex < properties.Count(); propertyIndex++)
			{
				GetType().GetProperty(properties[propertyIndex].Name).SetValue(this, properties[propertyIndex].GetValue(propertyInterface, null), null);
			}
			return await Services.PopupService.GetInstance().PushAsync<DualResponseViewModel, TPopupPage, bool>(this);
		}



		/// <summary>
		/// Provides a base dictionary, along with types that you can use to Initialise properties
		/// </summary>
		/// <returns>All Properties contained within the Viewmodel, with their names, current values, and types</returns>
		public virtual Dictionary<string, (object property, Type propertyType)> PullViewModelProperties()
		{
			return base.PullViewModelProperties<DualResponseViewModel>();
		}

		/// <summary>
		/// provides the DualResponsePopupPage Generic Type argument to
		/// <see cref="AutoGenerateBasicPopup{TPopupPage}(Color, Color, string, Color, Color, string, Color, string, string, int, int)"/>
		/// </summary>
		/// <param name="leftButtonColour"></param>
		/// <param name="leftButtonTextColour"></param>
		/// <param name="leftButtonText"></param>
		/// <param name="rightButtonColour"></param>
		/// <param name="rightButtonTextColour"></param>
		/// <param name="rightButtonText"></param>
		/// <param name="mainPopupColour">Background colour</param>
		/// <param name="popupInformation">Information Displayed to inform the user what they are making a decision on</param>
		/// <param name="displayImageName">If an image is wanted, can add it in</param>
		/// <param name="heightRequest">the height of the popup</param>
		/// <param name="widthRequest">the width</param>
		/// <returns> a task awaiting user interaction</returns>
		public static async Task<bool> AutoGenerateBasicPopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await AutoGenerateBasicPopup<DualResponsePopupPage>(leftButtonColour, leftButtonTextColour, leftButtonText, rightButtonColour, rightButtonTextColour, rightButtonText, mainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}

		/// <summary>
		/// Attached basic properties defined.
		/// Left button will return false
		/// Right button will return true
		/// </summary>
		/// <typeparam name="TPopupPage">User defined page that uses the DualResponseViewModel ViewModel</typeparam>
		/// <param name="leftButtonColour"></param>
		/// <param name="leftButtonTextColour"></param>
		/// <param name="leftButtonText"></param>
		/// <param name="rightButtonColour"></param>
		/// <param name="rightButtonTextColour"></param>
		/// <param name="rightButtonText"></param>
		/// <param name="mainPopupColour">Background colour</param>
		/// <param name="popupInformation">Information Displayed to inform the user what they are making a decision on</param>
		/// <param name="displayImageName">If an image is wanted, can add it in</param>
		/// <param name="heightRequest">the height of the popup</param>
		/// <param name="widthRequest">the width</param>
		/// <returns> a task awaiting user interaction</returns>
		public static async Task<bool> AutoGenerateBasicPopup<TPopupPage>(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color mainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0) where TPopupPage : Rg.Plugins.Popup.Pages.PopupPage, IGenericViewModel<DualResponseViewModel>, new()
		{
			var AutoGeneratePopupViewModel = new DualResponseViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(false));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<TPopupPage>(true));

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
				{ "MainPopupInformation", popupInformation ?? "An Error has occured, try again" },
				{ "MainPopupColour", mainPopupColour },
				{ "PictureSource", displayImageName ?? "NoSource.png" }
			};
			return await AutoGeneratePopupViewModel.GeneratePopup<TPopupPage>(propertyDictionary);
		}










		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task<bool> leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task<bool> rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new DualResponseViewModel(Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(leftButtonTask));
			ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(rightButtonTask));

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
				{ "MainPopupInformation", popupInformation ?? "An Error has occured, try again" },
				{ "MainPopupColour", MainPopupColour },
				{ "PictureSource", displayImageName ?? "NoSource.png" }
			};

			return await AutoGeneratePopupViewModel.GeneratePopup(propertyDictionary);
		}


		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new DualResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(false));
			ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(true));
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new DualResponseViewModel(Services.PopupService.GetInstance());
			AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
			{
				await leftButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(false);
			});

			AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
			{
				await rightButtonTask;
				AutoGeneratePopupViewModel.SafeCloseModal<DualResponsePopupPage>(true);
			});
			return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText,
			ICommand leftButtonCommand,
			Color rightButtonColour,
			Color rightButtonTextColour,
			string rightButtonText,
			ICommand rightButtonCommand,
			Color MainPopupColour,
			string popupInformation,
			string displayImageName,
			int heightRequest,
			int widthRequest,
			DualResponseViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<DualResponseViewModel, DualResponsePopupPage, bool>(AutoGeneratePopupViewModel);
		}

		[Obsolete("phasing out, making API simplier and easier to upgrade")]
		private static void PropertySetter(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest, int widthRequest, DualResponseViewModel AutoGeneratePopupViewModel)
		{


			AutoGeneratePopupViewModel.LeftButtonCommand = leftButtonCommand;
			AutoGeneratePopupViewModel.LeftButtonColour = leftButtonColour;
			AutoGeneratePopupViewModel.LeftButtonText = leftButtonText ?? "Yes";
			AutoGeneratePopupViewModel.LeftButtonTextColour = leftButtonTextColour;

			AutoGeneratePopupViewModel.RightButtonCommand = rightButtonCommand;
			AutoGeneratePopupViewModel.RightButtonColour = rightButtonColour;
			AutoGeneratePopupViewModel.RightButtonText = rightButtonText ?? "No";
			AutoGeneratePopupViewModel.RightButtonTextColour = rightButtonTextColour;

			AutoGeneratePopupViewModel.HeightRequest = heightRequest;
			AutoGeneratePopupViewModel.WidthRequest = widthRequest;
			AutoGeneratePopupViewModel.MainPopupInformation = popupInformation ?? "An Error has occured, try again";
			AutoGeneratePopupViewModel.MainPopupColour = MainPopupColour;
			AutoGeneratePopupViewModel.PictureSource = displayImageName ?? "NoSource.png";
		}



		[Obsolete("phasing out, making API simplier and easier to upgrade, throwing errors due to the use of Obsolete structs, want to encourage their removal")]
		public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task<bool> leftButtonTask, Task<bool> rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade, throwing errors due to the use of Obsolete structs, want to encourage their removal")]
		public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}
		[Obsolete("phasing out, making API simplier and easier to upgrade, throwing errors due to the use of Obsolete structs, want to encourage their removal")]
		public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task leftButtonTask, Task rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}
	}
}

