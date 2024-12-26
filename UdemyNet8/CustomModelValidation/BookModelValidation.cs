using System.ComponentModel.DataAnnotations;
using UdemyNet8.api.model;

namespace UdemyNet8.CustomModelValidation
{
    public class BookModelValidation : ValidationAttribute
    {
        //public override bool IsValid(object? value)
        //{
        //    var a = value == null ? null : value.GetType();
        //    if (a == null || a != typeof(Book)) return false;
        //    if (((Book)value!).Title == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public string? ErrorMessageBookNull { get; set; } = "Error while the book is null";
        public string? ErrorMessageTitleNull { get; set; } = "Error while book's title is null";
        public BookModelValidation(){}

        public BookModelValidation(string? errorMessageBookNull, string? errorMessageTitleNull)
        {
            ErrorMessageBookNull = errorMessageBookNull;
            ErrorMessageTitleNull = errorMessageTitleNull;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var a = value == null ? null : value.GetType();
            if (a == null || a != typeof(Book)) return new ValidationResult(this.ErrorMessageBookNull);
            if (((Book)value!).Title == null)
            {
                return new ValidationResult(this.ErrorMessageTitleNull);
            }
            return ValidationResult.Success;
        }
    }
}
