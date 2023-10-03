using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.ValidationAttributes
{
    public class MoreThanFiveWords : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string)
            {
                string inputValue = (string)value;

                string[] words = inputValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputValue == null || words.Length < 5)
                {
                    return new ValidationResult("Description must have at least five words.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Description has to be a text");
        }
    }
}
