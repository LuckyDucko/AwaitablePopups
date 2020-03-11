using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using AwaitablePopups.Structs;
using Xamarin.Forms;

namespace AwaitablePopups.PopupPages.DualResponse
{
    public sealed class DualResponseViewModel : PopupViewModel<bool>
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

        private static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color MainPopupColour, string popupInformation, string displayImageName, DualResponseViewModel AutoGeneratePopupViewModel)
        {
            PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, AutoGeneratePopupViewModel);
            return await Services.PopupService.GetInstance().PushAsync<DualResponseViewModel, DualResponsePopupPage, bool>(AutoGeneratePopupViewModel);
        }

        public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task<bool> leftButtonTask, Task<bool> rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, popupInformation, displayImageName);
        }

        public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, MainPopupColour, popupInformation, displayImageName);
        }

        public static async Task<bool> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task leftButtonTask, Task rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, popupInformation, displayImageName);
        }

        public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task<bool> leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task<bool> rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            var AutoGeneratePopupViewModel = new DualResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
            ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal(leftButtonTask));
            ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal(rightButtonTask));
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, AutoGeneratePopupViewModel);
        }

        public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            var AutoGeneratePopupViewModel = new DualResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
            ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal(true));
            ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal(false));
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, AutoGeneratePopupViewModel);
        }

        public static async Task<bool> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task rightButtonTask, Color MainPopupColour, string popupInformation, string displayImageName)
        {
            var AutoGeneratePopupViewModel = new DualResponseViewModel(AwaitablePopups.Services.PopupService.GetInstance());
            AsyncCommand leftButtonCommand = new AsyncCommand(async () =>
            {
                await leftButtonTask;
                AutoGeneratePopupViewModel.SafeCloseModal(true);
            });

            AsyncCommand rightButtonCommand = new AsyncCommand(async () =>
            {
                await rightButtonTask;
                AutoGeneratePopupViewModel.SafeCloseModal(false);
            });
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, popupInformation, displayImageName, AutoGeneratePopupViewModel);
        }


        private static void PropertySetter(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color MainPopupColour, string popupInformation, string displayImageName, DualResponseViewModel AutoGeneratePopupViewModel)
        {
            AutoGeneratePopupViewModel.LeftButtonCommand = leftButtonCommand;
            AutoGeneratePopupViewModel.LeftButtonColour = leftButtonColour;
            AutoGeneratePopupViewModel.LeftButtonText = leftButtonText ?? "Yes";
            AutoGeneratePopupViewModel.LeftButtonTextColour = leftButtonTextColour;

            AutoGeneratePopupViewModel.RightButtonCommand = rightButtonCommand;
            AutoGeneratePopupViewModel.RightButtonColour = rightButtonColour;
            AutoGeneratePopupViewModel.RightButtonText = rightButtonText ?? "No";
            AutoGeneratePopupViewModel.RightButtonTextColour = rightButtonTextColour;

            AutoGeneratePopupViewModel.MainPopupInformation = popupInformation ?? "An Error has occured, try again";
            AutoGeneratePopupViewModel.MainPopupColour = MainPopupColour;
            AutoGeneratePopupViewModel.PictureSource = displayImageName ?? "NoSource.png";
        }
    }
}

