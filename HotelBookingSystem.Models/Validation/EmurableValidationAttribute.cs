using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal class EmurableValidationAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }
        
        public override bool IsValid(object? value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IEnumerable<object> && value != null)
                if (((IEnumerable<object>)value).Count() == 0)
                    return ValidationResult.Success;
            return base.IsValid(value, validationContext);
        }
    }
}
