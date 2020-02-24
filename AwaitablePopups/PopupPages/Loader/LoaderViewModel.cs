using System;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;

namespace AwaitablePopups.PopupPages.Loader
{
    public class LoaderViewModel : PopupViewModel<bool>
    {
        private Xamarin.Forms.Color _loaderColour;
        public Xamarin.Forms.Color LoaderColour
        {
            get => _loaderColour;
            set => SetValue(ref _loaderColour, value);
        }

        private Xamarin.Forms.Color _textColour;
        public Xamarin.Forms.Color TextColour
        {
            get => _textColour;
            set => SetValue(ref _textColour, value);
        }


        public LoaderViewModel(IPopupService popupService) : base(popupService)
        {
        }

    }
}
