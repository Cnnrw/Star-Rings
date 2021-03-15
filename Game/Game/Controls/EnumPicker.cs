using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using Xamarin.Forms;

namespace Game.Controls
{
    class EnumPicker<T> : Picker
        where T : struct
    {
        public EnumPicker()
        {
            BindingContextChanged += EnumPicker_BindingContextChanged;
            SelectedIndexChanged += OnSelectedIndexChanged;

            // Fill the items from the enum
            foreach (var value in Enum.GetValues(typeof(T)))
                Items.Add(GetEnumDescription(value));
        }

        void EnumPicker_BindingContextChanged(object sender, EventArgs e) =>
            /*
             * if the current value is the same as the default, it wouldn't
             * recognize the change. Force OnSelectedItemChanged to handle this
             * case.
             */
            OnSelectedItemChanged(this, SelectedItem, SelectedItem);

        public new static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(propertyName: nameof(SelectedItem),
                                    returnType: typeof(T),
                                    declaringType: typeof(EnumPicker<T>),
                                    defaultValue: default(T),
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: OnSelectedItemChanged);

        public new T SelectedItem
        {
            get => (T)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        static void OnSelectedItemChanged(BindableObject bo, object o, object n)
        {
            if (!(bo is EnumPicker<T> picker))
                return;

            picker.SelectedIndex = picker.Items.IndexOf(GetEnumDescription(n));
        }

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = default;
                return;
            }

            // try parsing, if using description this will fail
            if (!Enum.TryParse(Items[SelectedIndex], out T match))
                // find enum by description
                match = GetEnumByDescription(Items[SelectedIndex]);

            SelectedItem = (T)Enum.Parse(typeof(T), match.ToString());

        }

        static string GetEnumDescription(object value)
        {
            var result = value.ToString();
            var attribute = typeof(T).GetRuntimeField(value.ToString())
                                     .GetCustomAttributes<DisplayAttribute>(false)
                                     .SingleOrDefault();

            if (attribute != null)
                result = attribute.Description;

            return result;
        }

        static T GetEnumByDescription(string description) =>
            Enum.GetValues(typeof(T))
                .Cast<T>()
                .FirstOrDefault(x => string.Equals(GetEnumDescription(x), description));
    }
}
