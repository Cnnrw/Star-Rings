using Xamarin.Forms;

namespace Game.Helpers.Behaviors
{
    public class EntryLineValidationBehavior : BehaviorBase<Entry>
    {
        #region Static Fields

        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool),
            typeof(EntryLineValidationBehavior), true, BindingMode.Default, null,
            (bindable, oldValue, newValue) => OnIsValidChanged(bindable, newValue));

        #endregion
        #region Properties

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        #endregion
        #region Static Methods

        private static void OnIsValidChanged(BindableObject bindable, object newValue)
        {
            if (bindable is EntryLineValidationBehavior IsValidBehavior &&
                newValue is bool IsValid)
            {
                IsValidBehavior.AssociatedObject.PlaceholderColor = IsValid ? Color.Default : Color.Red;
            }
        }

        #endregion
    }
}