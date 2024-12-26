using System.ComponentModel.DataAnnotations;

namespace UdemyNet8.CustomModelValidation
{
    public class DateRangeModelValidate : ValidationAttribute
    {
        public string? OtherPropertyName { get; set; } 

        public DateRangeModelValidate(string otherPropertyName)
        {
            this.OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var endDate = Convert.ToDateTime(value);

                var otherProperty = validationContext.ObjectType.GetProperty(this.OtherPropertyName!);

                DateTime fromDate = Convert.ToDateTime(otherProperty!.GetValue(validationContext.ObjectInstance));

                if(fromDate > endDate)
                {
                    return new ValidationResult(ErrorMessage);
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}
