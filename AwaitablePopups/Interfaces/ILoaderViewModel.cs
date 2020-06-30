using System.Collections.Generic;

using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
    public interface ILoaderViewModel
    {
        Color LoaderColour { get; set; }
        Color TextColour { get; set; }
        List<string> ReasonsForLoader { get; set; }
        int MillisecondsBetweenReasonSwitch { get; set; }
        void SafeCloseModal();
    }
}