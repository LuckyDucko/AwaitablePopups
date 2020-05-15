using System.Windows.Input;
using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
	public interface IDualResponseViewModel
	{
		ICommand LeftButtonCommand { get; set; }
		string LeftButtonText { get; set; }
		Color LeftButtonColour { get; set; }
		Color LeftButtonTextColour { get; set; }
		ICommand RightButtonCommand { get; set; }
		string RightButtonText { get; set; }
		string PictureSource { get; set; }
		Color RightButtonColour { get; set; }
		Color RightButtonTextColour { get; set; }
	}
}