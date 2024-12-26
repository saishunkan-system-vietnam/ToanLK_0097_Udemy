using CrudSection.Model;
using CrudSection.Services.IService;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace CrudSection.Services.Service
{
    public class PersonService : IPersonService
    {
        private List<Person> _person;
        public PersonService(bool init = true)
        {
            _person = new List<Person>();
            if (init)
            {
                _person.Add(
                    new Person(
                    Guid.NewGuid(),
                    "Le Khanh Toan",
                    12,
                    "Farmer",
                    new Country()
                    )
                );
                _person.Add(
                    new Person(
                    Guid.NewGuid(),
                    "Le Khanh Toan",
                    21,
                    "Miner",
                    new Country()
                    )
                );
                _person.Add(
                    new Person(
                    Guid.NewGuid(),
                    "Toan Khanh Le",
                    42,
                    "Explorer",
                    new Country()
                    )
                );
                _person.Add(
                    new Person(
                    Guid.NewGuid(),
                    "Khanh Le Toan",
                    52,
                    "Fighter",
                    new Country()
                    )
                );
                _person.Add(
                    new Person(
                    Guid.NewGuid(),
                    "Toan Le Khanh",
                    542,
                    "Traveler",
                    new Country()
                    )
                );
            }

        }

        public Person AddPerson(Person addPerson)
        {
            _person.Add(addPerson);
            return addPerson;
        }

        public Guid DeletePerson(Guid PersonId)
        {
            Person deletePerson = this.GetById(PersonId);
            if(deletePerson != null)
            {
                _person.Remove(deletePerson);
            }
            return PersonId;
        }

        public Person? GetById(Guid personId)
        {
            Person? returnPerson = null;
            foreach (Person person in _person)
            {
                if (person.Id == personId)
                {
                    returnPerson = person;
                    break;
                }
            }
            return returnPerson;
        }

        public List<Person> GetPerson()
        {
            return _person;
        }

        public Person? UpdatePerson(Person updatedPerson, Guid updatedId)
        {
            Person? person = this.GetById(updatedId);
            if(person != null)
            {
                int index = _person.IndexOf(person);
                _person[index].Name = updatedPerson.Name;
                _person[index].Age = updatedPerson.Age;
                _person[index].Job = updatedPerson.Job;
                _person[index].Country = updatedPerson.Country;
                return _person[index];
            }
            return null;
        }
        public List<Person> GetFilter(string SearchBy, string keyword)
        {
            List<Person> allPerson = this.GetPerson();
            List<Person> filterPerson = allPerson;

            switch (SearchBy)
            {
                case nameof(Person.Id):
                    filterPerson = allPerson.Where(temp => temp.Id.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case nameof(Person.Name):
                    filterPerson = allPerson.Where(temp => temp.Name.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.Job):
                    filterPerson = allPerson.Where(temp => temp.Job.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
                    break;

                case nameof(Person.Age):
                    filterPerson = allPerson.Where(temp => temp.Age.ToString() == keyword).ToList<Person>();
                    break;
            }
            return filterPerson;
        }

        public List<Person> GetSort(List<Person> list, string sortBy, int sortOrder)
        {
            List<Person> sortedList = list;
            switch (sortBy) {
                case nameof(Person.Id):
                    if(sortOrder == 0)
                    {
                        sortedList = list.OrderBy((x) => x.Id).ToList();
                    }
                    else
                    {
                        sortedList = list.OrderByDescending((x) => x.Id).ToList();
                    }
                    break;
                case nameof(Person.Name):
                    if (sortOrder == 0)
                    {
                        sortedList = list.OrderBy((x) => x.Name).ToList();
                    }
                    else
                    {
                        sortedList = list.OrderByDescending((x) => x.Name).ToList();
                    }
                    break;

                case nameof(Person.Job):
                    if (sortOrder == 0)
                    {
                        sortedList = list.OrderBy((x) => x.Job).ToList();
                    }
                    else
                    {
                        sortedList = list.OrderByDescending((x) => x.Job).ToList();
                    }
                    break;

                case nameof(Person.Age):
                    if (sortOrder == 0)
                    {
                        sortedList = list.OrderBy((x) => x.Age).ToList();
                    }
                    else
                    {
                        sortedList = list.OrderByDescending((x) => x.Age).ToList();
                    }
                    break;
            }
            return sortedList;
        }
    }
}
