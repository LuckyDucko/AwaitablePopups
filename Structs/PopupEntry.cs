
using System;

namespace AwaitablePopups.Structs
{
	[Obsolete("phasing out, not compatible with non-obsolete API")]
	public struct PopupEntry : IEquatable<PopupEntry>
	{
		public string EntryText { get; set; }
		public string EntryPlaceholder { get; set; }
		public Xamarin.Forms.Color EntryTextColour { get; set; }
		public Xamarin.Forms.Color PlaceholderTextColour { get; set; }
		public Xamarin.Forms.Color BackgroundColour { get; set; }

		public PopupEntry(string entryText, string entryPlaceholder, Xamarin.Forms.Color entryTextColour, Xamarin.Forms.Color placeholderTextColour, Xamarin.Forms.Color backgroundColour)
		{
			EntryText = entryText ?? throw new System.ArgumentNullException(nameof(entryText));
			EntryPlaceholder = entryPlaceholder ?? throw new System.ArgumentNullException(nameof(entryPlaceholder));
			EntryTextColour = entryTextColour;
			PlaceholderTextColour = placeholderTextColour;
			BackgroundColour = backgroundColour;
		}

		public bool Equals(PopupEntry other)
		{
			return EntryText.Equals(other.EntryText)
				&& EntryPlaceholder.Equals(other.EntryPlaceholder)
				&& EntryTextColour.Equals(other.EntryTextColour)
				&& PlaceholderTextColour.Equals(other.PlaceholderTextColour)
				&& BackgroundColour.Equals(other.BackgroundColour);
		}
	}
}
