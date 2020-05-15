using System.Windows.Input;
using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
	public interface ISingleResponseViewModel
	{
		ICommand SingleButtonCommand { get; set; }
		string SingleButtonText { get; set; }
		Color SingleButtonColour { get; set; }
		Color SingleButtonTextColour { get; set; }
		string SingleDisplayImage { get; set; }
	}
}