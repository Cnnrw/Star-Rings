using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Game.Validators
{
    public class ValidatableObject<T> : IValidatable<T>
    {

        private T _value;

        public bool CleanOnChange { get; set; } = true;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                if (CleanOnChange)
                {
                    IsValid = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();

        public List<string> Errors { get; set; } = new List<string>();

        public bool IsValid { get; set; } = true;

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                                                    .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }

        public override string ToString() => $"{Value}";
    }
}