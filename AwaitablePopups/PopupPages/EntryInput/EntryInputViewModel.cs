using System;
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

        private static async Task<string> GeneratePopup(Color leftButtonColour,
                                                  Color leftButtonTextColour,
                                                  string leftButtonText,
                                                  ICommand leftButtonCommand,
                                                  Color rightButtonColour,
                                                  Color rightButtonTextColour,
                                                  string rightButtonText,
                                                  ICommand rightButtonCommand,
                                                  Color MainPopupColour,
                                                  string DefaultTextInput,
                                                  string DefaultPlaceHolder,
                                                  EntryInputViewModel AutoGeneratePopupViewModel)
        {
            PropertySetter(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, DefaultTextInput, DefaultPlaceHolder, AutoGeneratePopupViewModel);
            return await Services.PopupService.GetInstance().PushAsync<EntryInputViewModel, EntryInputPopupPage, string>(AutoGeneratePopupViewModel);
        }

        public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task<bool> leftButtonTask, Task<bool> rightButtonTask, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, DefaultTextInput, DefaultPlaceHolder);
        }

        public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, MainPopupColour, DefaultTextInput, DefaultPlaceHolder);
        }

        public static async Task<string> GeneratePopup(PopupButton leftButton, PopupButton rightButton, Task leftButtonTask, Task rightButtonTask, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
        {
            return await GeneratePopup(leftButton.ButtonColour, leftButton.ButtonTextColour, leftButton.ButtonText, leftButtonTask, rightButton.ButtonColour, rightButton.ButtonTextColour, rightButton.ButtonText, rightButtonTask, MainPopupColour, DefaultTextInput, DefaultPlaceHolder);
        }

        public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task<string> leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task<string> rightButtonTask, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
        {
            var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
            ICommand leftButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(leftButtonTask));
            ICommand rightButtonCommand = new AsyncCommand(async () => await AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(rightButtonTask));
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, DefaultTextInput, DefaultPlaceHolder, AutoGeneratePopupViewModel);
        }

        public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
        {
            var AutoGeneratePopupViewModel = new EntryInputViewModel(Services.PopupService.GetInstance());
            ICommand leftButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>("No Text Available"));
            ICommand rightButtonCommand = new Command(() => AutoGeneratePopupViewModel.SafeCloseModal<EntryInputPopupPage>(AutoGeneratePopupViewModel.TextInput));
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, DefaultTextInput, DefaultPlaceHolder, AutoGeneratePopupViewModel);
        }

        public static async Task<string> GeneratePopup(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, Task leftButtonTask, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, Task rightButtonTask, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder)
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
            return await GeneratePopup(leftButtonColour, leftButtonTextColour, leftButtonText, leftButtonCommand, rightButtonColour, rightButtonTextColour, rightButtonText, rightButtonCommand, MainPopupColour, DefaultTextInput, DefaultPlaceHolder, AutoGeneratePopupViewModel);
        }


        private static void PropertySetter(Color leftButtonColour, Color leftButtonTextColour, string leftButtonText, ICommand leftButtonCommand, Color rightButtonColour, Color rightButtonTextColour, string rightButtonText, ICommand rightButtonCommand, Color MainPopupColour, string DefaultTextInput, string DefaultPlaceHolder, EntryInputViewModel AutoGeneratePopupViewModel)
        {
            AutoGeneratePopupViewModel.LeftButtonCommand = leftButtonCommand;
            AutoGeneratePopupViewModel.LeftButtonColour = leftButtonColour;
            AutoGeneratePopupViewModel.LeftButtonText = leftButtonText ?? "No";
            AutoGeneratePopupViewModel.LeftButtonTextColour = leftButtonTextColour;

            AutoGeneratePopupViewModel.RightButtonCommand = rightButtonCommand;
            AutoGeneratePopupViewModel.RightButtonColour = rightButtonColour;
            AutoGeneratePopupViewModel.RightButtonText = rightButtonText ?? "Yes";
            AutoGeneratePopupViewModel.RightButtonTextColour = rightButtonTextColour;

            AutoGeneratePopupViewModel.MainPopupColour = MainPopupColour;
            AutoGeneratePopupViewModel.TextInput = DefaultTextInput;
            AutoGeneratePopupViewModel.PlaceHolderInput = DefaultPlaceHolder;
        }
    }
}

