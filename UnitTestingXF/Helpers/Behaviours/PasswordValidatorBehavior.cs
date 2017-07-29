using System;

using Xamarin.Forms;

namespace UnitTestingXF.Helpers
{
	public class PasswordValidationBehavior : Behavior<Entry>
	{
		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
			base.OnAttachedTo(bindable);
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			((Entry)sender).TextColor = PasswordValidator.IsValidPassword(e.NewTextValue) ? Color.Default : Color.Red;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
			base.OnDetachingFrom(bindable);
		}
	}
}

