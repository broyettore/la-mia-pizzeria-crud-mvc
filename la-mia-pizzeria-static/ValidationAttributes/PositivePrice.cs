using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.ValidationAttributes
{
    public class PositivePrice : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is double)
            {
                double inputValue = (double)value;

                if(inputValue <= 0)
                {
                    return new ValidationResult("Price has to be bigger than 0");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Price has to be a number");
        }
    }
}
