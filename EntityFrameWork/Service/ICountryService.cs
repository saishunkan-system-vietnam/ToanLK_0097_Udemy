
using EntityFrameWork.Entities;

namespace EntityFrameWork.Service
{
    public interface ICountryService
    {
        public List<Country> GetAllCountries();
        public Country? GetById(Guid? countryId);
        public Country AddCountry(Country country);
    }
}
