using CrudSection.Model;
using CrudSection.Services.IService;

namespace CrudSection.Services.Service
{
    public class CountryService : ICountryService
    {
        private List<Country> _countries;
        public CountryService(bool init = true)
        {
            _countries = new List<Country>();
            if (init)
            {
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "Viet Nam",
                    "9000"
                    )
                );
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "Japan",
                    "5724"
                    )
                );
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "USA",
                    "9831"
                    )
                );
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "Korean",
                    "9312"
                    )
                );
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "DenMark",
                    "1984"
                    )
                );
                _countries.Add(
                    new Country(
                    Guid.NewGuid(),
                    "China",
                    "8422"
                    )
                );
            }

        }

        public Country AddCountry(Country addCountry)
        {
            _countries.Add(addCountry);
            return addCountry;
        }

        public Guid DeleteCountry(Guid CountryId)
        {
            Country deleteCountry = this.GetById(CountryId);
            if(deleteCountry != null)
            {
                _countries.Remove(deleteCountry);
            }
            return CountryId;
        }

        public Country? GetById(Guid countryId)
        {
            Country? returnCountry = null;
            foreach (Country country in _countries)
            {
                if (country.CountryId == countryId)
                {
                    returnCountry = country;
                    break;
                }
            }
            return returnCountry;
        }

        public List<Country> GetCountries()
        {
            return _countries;
        }

        public Country? UpdateCountry(Country updatedCountry, Guid updatedId)
        {
            Country? country = this.GetById(updatedId);
            if(country != null)
            {
                int index = _countries.IndexOf(country);
                _countries[index].CountryName = updatedCountry.CountryName;
                _countries[index].CountryCode = updatedCountry.CountryCode;
                return _countries[index];
            }
            return null;
        }
    }
}
