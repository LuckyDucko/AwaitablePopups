using System.Windows.Input;
using Xamarin.Forms;

namespace AwaitablePopups.Interfaces
{
	public interface ILoginViewModel
	{
		string Username { get; set; }
		string UsernamePlaceholder { get; set; }
		Color UsernamePlaceholderColour { get; set; }
		Color UsernameTextColour { get; set; }
		Color UsernameBackgroundColour { get; set; }
		string Password { get; set; }
		string PasswordPlaceholder { get; set; }
		Color PasswordPlaceholderColour { get; set; }
		Color PasswordTextColour { get; set; }
		Color PasswordBackgroundColour { get; set; }
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