using System.Collections.Generic;
using System.ComponentModel;

namespace Game.Validators
{
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }

        bool IsValid { get; set; }

        bool Validate();
    }
}