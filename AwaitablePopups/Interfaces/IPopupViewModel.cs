using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
    public interface IPopupViewModel<TReturnable>
    {
        TaskCompletionSource<TReturnable> Returnable { get; set; }
        string MainPopupInformation { get; set; }
        Color MainPopupColour { get; set; }
        int WidthRequest { get; set; }
        int HeightRequest { get; set; }

        void InitialiseOptionalProperties(Dictionary<string, object> optionalProperties);
        void SafeCloseModal<TPopupType>() where TPopupType : Rg.Plugins.Popup.Pages.PopupPage, new();
        Task SafeCloseModal<TPopupType>(Task<TReturnable> buttonCommand) where TPopupType : Rg.Plugins.Popup.Pages.PopupPage, new();
        void SafeCloseModal<TPopupType>(TReturnable result) where TPopupType : Rg.Plugins.Popup.Pages.PopupPage, new();
    }
}