using System.ComponentModel.DataAnnotations;

namespace UdemyNet8.api.model
{
    public class ValidatableObject : IValidatableObject
    {
        [Required]
        [StringLength(12,MinimumLength =3)]
        public string? PersonName { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(PersonName == "hello")
            {
                yield return new ValidationResult("error");
            }
        }

        public override string ToString()
        {
            return $"{PersonName}\n{EmailAddress}";
        }
    }
}
