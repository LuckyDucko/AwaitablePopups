using System;
using System.Windows.Input;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using Xamarin.Forms;

namespace AwaitablePopups.PopupPages.SingleResponsePopupPage
{
    public sealed class SingleResponseViewModel : PopupViewModel<bool>
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

        private string _singleDisplayImage;
        public string SingleDisplayImage
        {
            get => _singleDisplayImage;
            set => SetValue(ref _singleDisplayImage, value);
        }

        public SingleResponseViewModel(IPopupService popupService) : base(popupService)
        {

        }
    }

}
