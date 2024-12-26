using CrudSection.Model;

namespace CrudSection.Services.IService
{
    public interface ICountryService
    {
        public Country AddCountry(Country addCountry);
        public Guid DeleteCountry(Guid CountryId);
        public List<Country> GetCountries();
        public Country UpdateCountry(Country updatedCountry, Guid updatedId);
        public Country GetById(Guid countryId);
    }
}
