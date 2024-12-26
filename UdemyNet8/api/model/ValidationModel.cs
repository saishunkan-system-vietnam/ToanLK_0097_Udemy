using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.ComponentModel.DataAnnotations;
using UdemyNet8.CustomModelValidation;

namespace UdemyNet8.api.model
{
    public class ValidationModel
    {
        [Required]
        public int? Id { get; set; }

        [StringLength(maximumLength: 12, MinimumLength = 12)]
        public string? Name { get; set; }

        [Range(4, 7)]
        public int Age { get; set; }

        [RegularExpression("^8[0-9]$")]
        public string RegularExpression { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        public string Phone { get; set; }

        [BookModelValidation]
        [ValidateNever]
        public Book? Book { get; set; }
        
        public DateTime StartDate {get;set;}

        public List<string> CollectionBinder { get; set; }

        [DateRangeModelValidate("StartDate",ErrorMessage ="'Start date'should have older than 'End Date'")]
        public DateTime EndDate { get; set; }
 
        public override string ToString()
        {
            string returnString = "";
            foreach (var property in this.GetType().GetProperties())
            {
                returnString += $"{property.Name}: {property.GetValue(this)?.ToString()}\n";
            }
            return returnString;
        }
        
    }

}
