using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using AwaitablePopups.Interfaces;

namespace AwaitablePopups.AbstractClasses
{
	public abstract class PopupViewModel<TReturnable> : BasePopupViewModel, IPopupViewModel<TReturnable>
	{
		/// <summary>
		/// This is what the result of the popupPage will be when it returns to its caller
		/// </summary>
		public TaskCompletionSource<TReturnable> Returnable { get; set; }

		/// <summary>
		/// This is the fallback value, incase of premature exists
		/// </summary>
		protected TReturnable BaseExitValue { get; set; }

		private string _mainPopupInformation;
		public string MainPopupInformation
		{
			get => _mainPopupInformation;
			set => SetValue(ref _mainPopupInformation, value);
		}

		private Xamarin.Forms.Color _mainPopupColour;
		public Xamarin.Forms.Color MainPopupColour
		{
			get => _mainPopupColour;
			set => SetValue(ref _mainPopupColour, value);
		}

		protected PopupViewModel(IPopupService popupService) : base(popupService)
		{
			Returnable = new TaskCompletionSource<TReturnable>();
			BaseExitValue = default(TReturnable);
		}


		public virtual void SafeCloseModal()
		{
			SafeCloseModal(BaseExitValue);
		}

		/// <summary>
		/// awaits the async action to complete, and then
		/// passes the result to the sync SafeCloseModal
		/// </summary>
		/// <param name="asyncResult">still processing results</param>
		/// <returns></returns>
		public virtual async Task SafeCloseModal(Task<TReturnable> asyncResult)
		{
			if (asyncResult.Status.Equals(TaskStatus.Created) || asyncResult.Status.Equals(TaskStatus.WaitingForActivation))
			{
				asyncResult.Start();
			}
			var buttonCommandResult = await asyncResult;
			SafeCloseModal(buttonCommandResult);
		}

		/// <summary>
		/// returns the result of the popup into the awaitable task, With
		/// fallback attempts if necessary.
		/// </summary>
		/// <param name="result">User Feedback/Processed Results</param>
		public virtual void SafeCloseModal(TReturnable result)
		{
			try
			{
				var safeCloseAttempt = Returnable.TrySetResult(result);
				if (!safeCloseAttempt)
				{
					Returnable = new TaskCompletionSource<TReturnable>();
					Returnable.SetResult(result);
				}
			}
			catch (Exception)
			{
				Returnable = new TaskCompletionSource<TReturnable>();
				Returnable.SetResult(BaseExitValue);
			}
			finally
			{
				PopupService.PopAsync();
			}

		}

		/// <summary>
		/// This is for use only when you wish for some form of reusable wrapper,
		/// it provides little protection or help.
		/// </summary>
		/// <param name="optionalProperties"></param>
		public virtual void InitialiseOptionalProperties(Dictionary<string, object> optionalProperties)
		{
			foreach (KeyValuePair<string, object> property in optionalProperties)
			{
				GetType().GetProperty(property.Key).SetValue(this, property.Value, null);
			}
		}

		/// <summary>
		/// This is for use only when you wish for some form of reusable wrapper,
		/// This method requires a specific type which it then will handle in a parallel fashion. 
		/// </summary>
		/// <typeparam name="TPropertyValue"></typeparam>
		/// <param name="optionalProperties"></param>
		public void InitialiseOptionalProperties<TPropertyValue>(Dictionary<string, TPropertyValue> optionalProperties)
		{
			optionalProperties
				.AsParallel()
				.ForAll((KeyValuePair<string, TPropertyValue> property)
					=> GetType()
						.GetProperty(property.Key)
						.SetValue(this, property.Value, null));
		}

		/// <summary>
		/// Allows you to gather the values of every property that is on the popupviewmodel
		/// into a key/value pair. Properties will need to be cast into their proper types
		/// </summary>
		/// <typeparam name="TViewModel"> Viwemodel Type you wish to iterate over</typeparam>
		/// <returns></returns>
		public Dictionary<string, object> PullViewModelProperties<TViewModel>()
			where TViewModel : BasePopupViewModel
		{
			var propertyDictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = typeof(TViewModel).GetProperties();
			properties.AsParallel().ForAll((property) => propertyDictionary.Add(property.Name, property.GetValue(this, null)));
			return propertyDictionary;
		}
	}
}
