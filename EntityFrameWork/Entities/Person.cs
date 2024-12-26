using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameWork.Entities
{
    public class Person
    {

        [Key]
        public Guid PersonId { get; set; }
        [StringLength(60)] //nvarchar(40) 
        public string? PersonName { get; set; }
        [StringLength(320)]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        //unique indentifirer
        public Guid? CountryId { get; set; }

        [Remote("ValidationAddress","Person",ErrorMessage = "Address's Length must be more than 12")]
        public string? Address { get; set; }
        //bit
        public bool ReceiveNewsLetter { get; set; }
        public string TIN{ get; set; }
        public virtual Country? Country { get; set; }
    }
}
