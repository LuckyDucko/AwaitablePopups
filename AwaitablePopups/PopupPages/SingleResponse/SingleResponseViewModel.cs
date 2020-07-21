using System.Threading.Tasks;
using System.Windows.Input;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using Xamarin.Forms;
using AwaitablePopups.Structs;
using AsyncAwaitBestPractices.MVVM;

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
		/// <returns></returns>
		private static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Color MainPopupColour, string popupInformation, string displayImageName, ICommand singleButtonCommand, int heightRequest, int widthRequest, SingleResponseViewModel AutoGeneratePopupViewModel)
		{
			PropertySetter(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
			return await Services.PopupService.GetInstance().PushAsync<SingleResponseViewModel, SingleResponsePopupPage, bool>(AutoGeneratePopupViewModel);
		}

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
		/// <returns></returns>
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
		/// <returns></returns>
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
		/// <returns></returns>
		public static async Task<bool> GeneratePopup(PopupButton singleButton, Task<bool> buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest, int widthRequest)
		{
			return await GeneratePopup(singleButton.ButtonColour, singleButton.ButtonTextColour, singleButton.ButtonText, buttonTask, MainPopupColour, popupInformation, displayImageName, heightRequest, widthRequest);
		}


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
		/// <returns></returns>
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
		public static async Task<bool> GeneratePopup(Color buttonColour, Color buttonTextColour, string buttonText, Task<bool> buttonTask, Color MainPopupColour, string popupInformation, string displayImageName, int heightRequest = 0, int widthRequest = 0)
		{
			var AutoGeneratePopupViewModel = new SingleResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
			AsyncCommand singleButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<SingleResponsePopupPage>(buttonTask));
			return await GeneratePopup(buttonColour, buttonTextColour, buttonText, MainPopupColour, popupInformation, displayImageName, singleButtonCommand, heightRequest, widthRequest, AutoGeneratePopupViewModel);
		}
	}
}