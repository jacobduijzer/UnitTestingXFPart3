using System;

using Xamarin.Forms;

namespace UnitTestingXF.Helpers
{
	public class EmailValidatorBehavior : Behavior<Entry>
	{
		static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidatorBehavior), false);

		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

		public bool IsValid
		{
			get { return (bool)base.GetValue(IsValidProperty); }
			private set { base.SetValue(IsValidPropertyKey, value); }
		}

		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += HandleTextChanged;
		}

		void HandleTextChanged(object sender, TextChangedEventArgs e)
		{
			((Entry)sender).TextColor = EmailValidator.IsValidEmail(e.NewTextValue) ? Color.Default : Color.Red;
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= HandleTextChanged;
		}
	}
}

