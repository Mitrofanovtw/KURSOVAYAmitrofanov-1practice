using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime <= DateTime.UtcNow)
                return new ValidationResult("Дата должна быть в будущем");
        }
        return ValidationResult.Success!;
    }
}