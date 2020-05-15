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

		void InitialiseOptionalProperties(Dictionary<string, object> optionalProperties);
		void SafeCloseModal();
		Task SafeCloseModal(Task<TReturnable> buttonCommand);
		void SafeCloseModal(TReturnable result);
	}
}