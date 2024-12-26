namespace CrudSection.Model
{
    public class Country
    {
        public Country() { }
        public Country(Guid countryId, string? countryName, string? cOuntryCode)
        {
            CountryId = countryId;
            CountryName = countryName;
            CountryCode = cOuntryCode;
        }

        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
    }
}
