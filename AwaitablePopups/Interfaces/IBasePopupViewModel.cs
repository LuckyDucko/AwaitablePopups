using System.ComponentModel;

namespace AwaitablePopups.Interfaces
{
	public interface IBasePopupViewModel
	{
		bool IsBusy { get; set; }

		event PropertyChangedEventHandler PropertyChanged;
	}
}