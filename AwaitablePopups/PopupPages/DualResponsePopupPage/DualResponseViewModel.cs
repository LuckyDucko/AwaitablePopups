using System.Windows.Input;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;

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
    }
}

