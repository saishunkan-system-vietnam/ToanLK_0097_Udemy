using EntityFrameWork.Context;
using EntityFrameWork.Entities;

namespace EntityFrameWork.Service
{
    public class CountryService : ICountryService
    {
        private readonly PersonDbContext _db;
        public CountryService(PersonDbContext db)
        {
            _db = db;
        }

        public List<Country> GetAllCountries()
        {
            var country = _db.Countries.FirstOrDefault();
            foreach(var i  in country.Person)
            {
                Console.WriteLine(i.PersonName);
            }

            return _db.Countries.ToList<Country>();
        }

        public Country? GetById(Guid? countryId)
        {
            return _db.Countries.FirstOrDefault(country => country.CountryId == countryId);
        }

        public Country AddCountry(Country country)
        {
            country.CountryId = Guid.NewGuid();
            _db.Countries.Add(country);
            _db.SaveChanges();
            return country;
        }
    }
}
