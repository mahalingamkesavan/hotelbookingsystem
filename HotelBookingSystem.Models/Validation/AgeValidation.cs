using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models.Validation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class AgeValidation : ValidationAttribute
    {
        public AgeValidation()
        {
        }

        public AgeValidation(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public override bool IsValid(object? value)
        {
            int Age = 0;
            if (value is not int)
            {
                return false;
            }
            Age = (int)value;
            if (Age >= Limit)
                return true;
            return false;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int Age = 0;
            if (value is not int)
            {
                return new ValidationResult("Age is a number");
            }
            Age = (int)value;
            if (Age >= Limit)
                return ValidationResult.Success;
            return new ValidationResult("Under age");
        }
    }
}
