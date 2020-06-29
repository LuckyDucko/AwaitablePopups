using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AwaitablePopups.AbstractClasses;
using AwaitablePopups.Interfaces;

namespace AwaitablePopups.PopupPages.Loader
{
	public class LoaderViewModel : PopupViewModel<bool>, ILoaderViewModel
	{
		private Xamarin.Forms.Color _loaderColour;
		public Xamarin.Forms.Color LoaderColour
		{
			get => _loaderColour;
			set => SetValue(ref _loaderColour, value);
		}

		private Xamarin.Forms.Color _textColour;
		public Xamarin.Forms.Color TextColour
		{
			get => _textColour;
			set => SetValue(ref _textColour, value);
		}

		private IEnumerable<string> _reasonsForLoader;
		public IEnumerable<string> ReasonsForLoader
		{
			get => _reasonsForLoader;
			set => SetValue(ref _reasonsForLoader, value);
		}

		private int _millisecondsBetweenReasonSwitch;
		public int MillisecondsBetweenReasonSwitch
		{
			get => _millisecondsBetweenReasonSwitch;
			set => SetValue(ref _millisecondsBetweenReasonSwitch, value);
		}

		private CancellationTokenSource TextColourToken { get; set; }

		public LoaderViewModel(IPopupService popupService, IEnumerable<string> reasonsForLoader) : base(popupService)
		{
			TextColourToken = new CancellationTokenSource();
			ReasonsForLoader = reasonsForLoader;
			MainPopupInformation = ReasonsForLoader.Last();
			if ((reasonsForLoader?.Count() ?? 0) > 1)
			{
				Task.Run(() => InformationSwitch(TextColourToken)).ConfigureAwait(false);
			}
		}

		private void InformationSwitch(CancellationTokenSource TextToken)
		{
			while (!TextToken.IsCancellationRequested)
			{
				Thread.Sleep(MillisecondsBetweenReasonSwitch);
				for (int i = 1; i < 10; i++)
				{
					Thread.Sleep(50);
					TextColour = TextColour.WithLuminosity(i * 0.1);
				}

				int SelectionFromUnchosen = new Random().Next(ReasonsForLoader.Count() - 2);
				MainPopupInformation = ReasonsForLoader
										.OrderBy(pushChosenToBack)
										.ElementAt(SelectionFromUnchosen);
				for (int i = 10; i > 0; i--)
				{
					Thread.Sleep(50);
					TextColour = TextColour.WithLuminosity(i * 0.1);
				}
			}
			bool pushChosenToBack(string reasons) => !reasons.Equals(MainPopupInformation);
		}

		public override void SafeCloseModal()
		{
			TextColourToken.Cancel();
			base.SafeCloseModal(true);
		}
	}
}
