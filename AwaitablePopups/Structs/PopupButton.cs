using System;
using Xamarin.Forms;

namespace AwaitablePopups.Structs
{
    public struct PopupButton
    {
        public PopupButton(Color buttonColour, Color buttonTextColour, string buttonText)
        {
            ButtonColour = buttonColour;
            ButtonTextColour = buttonTextColour;
            ButtonText = buttonText;
        }

        public Xamarin.Forms.Color ButtonColour { get; set; }
        public Xamarin.Forms.Color ButtonTextColour { get; set; }
        public string ButtonText { get; set; }
    }
}
