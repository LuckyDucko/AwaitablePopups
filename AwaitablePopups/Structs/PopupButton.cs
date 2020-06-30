using System.Collections.Generic;

namespace AwaitablePopups.Structs
{
	public struct PopupButton
	{
		public Xamarin.Forms.Color ButtonColour { get; set; }
		public Xamarin.Forms.Color ButtonTextColour { get; set; }
		public string ButtonText { get; set; }

		public PopupButton(Xamarin.Forms.Color buttonColour, Xamarin.Forms.Color buttonTextColour, string buttonText)
		{
			ButtonColour = buttonColour;
			ButtonTextColour = buttonTextColour;
			ButtonText = buttonText;
		}
	}
}
