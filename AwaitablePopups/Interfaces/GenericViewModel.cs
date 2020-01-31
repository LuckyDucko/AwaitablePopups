using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using AwaitablePopups.AbstractClasses;

namespace AwaitablePopups.Interfaces
{
    public interface IGenericViewModel<TViewModel> where TViewModel : BasePopupViewModel
    {
        void SetViewModel(TViewModel viewModel);
        TViewModel GetViewModel();
    }
}