using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwaitablePopups.Interfaces;

namespace AwaitablePopups.AbstractClasses
{
    public abstract class PopupViewModel<TReturnable> : BasePopupViewModel
    {
        public TaskCompletionSource<TReturnable> Returnable { get; set; }
        protected TReturnable BaseExitValue { get; set; }

        private string _mainPopupInformation;
        public string MainPopupInformation
        {
            get => _mainPopupInformation;
            set => SetValue(ref _mainPopupInformation, value);
        }

        private Xamarin.Forms.Color _mainPopupColour;
        public Xamarin.Forms.Color MainPopupColour
        {
            get => _mainPopupColour;
            set => SetValue(ref _mainPopupColour, value);
        }

        protected PopupViewModel(IPopupService popupService) : base(popupService)
        {
            Returnable = new TaskCompletionSource<TReturnable>();
            BaseExitValue = default(TReturnable);
        }

        public virtual void SafeCloseModal()
        {
            SafeCloseModal(BaseExitValue);
        }

        public void SafeCloseModal(TReturnable result)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Returnable.SetResult(result);
                PopupService.PopAsync();
            }
        }
        /// <summary>
        /// This is for use only when you wish for some form of reusable wrapper,
        /// it provides little protection or help.
        /// </summary>
        /// <param name="optionalProperties"></param>
        public void InitialiseOptionalProperties(Dictionary<string, object> optionalProperties)
        {
            foreach (KeyValuePair<string, object> property in optionalProperties)
            {
                GetType().GetProperty(property.Key).SetValue(this, property.Value, null);
            }
        }
    }
}
