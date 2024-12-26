using CrudSection.Model;

namespace CrudSection.Services.IService
{
    public interface IPersonService
    {
        public Person AddPerson(Person addPerson);
        public Guid DeletePerson(Guid PersonId);
        public List<Person> GetPerson();
        public Person UpdatePerson(Person updatedPerson, Guid updatedId);
        public Person GetById(Guid countryId);
        public List<Person> GetFilter(string SearchBy, string keyword);
        public List<Person> GetSort(List<Person> list, string sortBy, int sortOrder);

    }
}
