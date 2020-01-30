using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Awaitable.Services
{
    public class PopupService : IPopupService
    {
        private static volatile PopupService s_internalPopupService = null;
        private static IPopupNavigation s_popupNavigation = null;
        private static readonly object s_threadPadlock = new object();
        private PopupPage TopPopupPage { get; set; }

        private PopupService(IPopupNavigation popupNavigation = null)
        {
            s_popupNavigation = popupNavigation ?? PopupNavigation.Instance;
            s_popupNavigation.Popping += RestrictPopAbility;
            s_popupNavigation.Popped += ResetTopPopupPage;
            s_popupNavigation.Pushed += ResetTopPopupPage;
        }

        private void ResetTopPopupPage(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            using var popupStack = s_popupNavigation.PopupStack.GetEnumerator();
            while (popupStack.MoveNext())
            {
                TopPopupPage = popupStack.Current;
            }
        }

        private void RestrictPopAbility(object sender, Rg.Plugins.Popup.Events.PopupNavigationEventArgs e)
        {
            TopPopupPage = null;
        }

        public static PopupService GetInstance(IPopupNavigation popupNavigation = null)
        {
            if (s_popupNavigation == null)
            {
                lock (s_threadPadlock)
                {
                    if (s_popupNavigation == null)
                    {
                        s_internalPopupService = new PopupService(popupNavigation);
                    }
                }
            }
            return s_internalPopupService;
        }

        public void PopAsync()
        {
            lock (s_threadPadlock)
            {
                if (TopPopupPage != null)
                {
                    s_popupNavigation.RemovePageAsync(TopPopupPage).SafeFireAndForget();
                }
            }
        }

        public TPopupPage CreatePopupPage<TPopupPage>() where TPopupPage : PopupPage, new()
        {
            return new TPopupPage();
        }

        public TPopupPage AttachViewModel<TPopupPage, TViewModel>(TPopupPage popupPage, TViewModel viewModel)
            where TPopupPage : PopupPage, IGenericViewModel<TViewModel>
            where TViewModel : BaseViewModel
        {
            popupPage.SetViewModel(viewModel);
            return popupPage;
        }

        /// <typeparam name="TViewModel">The ViewModel Type that is Attached to the Generic PopupPage</typeparam>
        /// <typeparam name="TPopupPage">The Generic Popupup that will be pushed onto the Navigation Stack</typeparam>
        /// <typeparam name="TReturnable">The Type that we expect to be return from the PopupPage through the generic ViewModel </typeparam>
        /// <param name="modalViewModel">The ViewModel that has been created</param>
        /// <returns>an Async Task that will wait for the PopupPage to return its value through its Viewmodel</returns>
        public async Task<TReturnable> PushAsync<TViewModel, TPopupPage, TReturnable>(TViewModel modalViewModel)
            where TPopupPage : PopupPage, IGenericViewModel<TViewModel>, new()
            where TViewModel : PopupViewModel<TReturnable>
        {
            TPopupPage popupModal = AttachViewModel(CreatePopupPage<TPopupPage>(), modalViewModel);
            await s_popupNavigation.PushAsync(popupModal);
            return await modalViewModel.Returnable.Task;
        }
    }
}
