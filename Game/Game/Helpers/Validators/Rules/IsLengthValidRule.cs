using System.Diagnostics;

namespace Game.Validators.Rules
{
    public class IsLengthValidRule<T> : IValidationRule<T>
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            Debug.Assert(str != null, nameof(str) + " != null");
            return str.Length > MinimumLength && str.Length <= MaximumLength;
        }
    }
}