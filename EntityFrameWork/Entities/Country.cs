using System.ComponentModel.DataAnnotations;

namespace EntityFrameWork.Entities
{
    public class Country
    {
        public Country(){}

        public Country(Guid countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
        }

        [Key]
        public Guid CountryId {get;set;}
        [StringLength(200)]
        public string CountryName {get;set;}
        public virtual ICollection<Person> Person { get; set; }
    }

}
