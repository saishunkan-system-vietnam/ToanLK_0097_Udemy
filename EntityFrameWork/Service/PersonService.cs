using EntityFrameWork.Context;
using EntityFrameWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EntityFrameWork.Service
{
    public class PersonService : IPersonService
    {
        private readonly PersonDbContext _db;
        public PersonService(PersonDbContext db)
        {
            _db = db;
        }

        public List<Person> GetAllPerson()
        {
            //return _db.sp_GetAllPerson();
            return _db.People.Include("Country").ToList();
        }

        public Person? GetById(Guid? id)
        {
            var result = _db.People.FirstOrDefault(item => item.PersonId == id);
            //var test = result.Country;
            return result;
        }

        public Person AddPerson(Person person)
        {
            person.PersonId = Guid.NewGuid();
            //_db.sp_InsertPerson(person);
            _db.People.Add(person);
            _db.SaveChanges();
            return person;
        }

        public Person? UpdatePerson(Person updatePerson)
        {
            Person? foundPerson = _db.People.FirstOrDefault((person) => person.PersonId == updatePerson.PersonId);
            if (foundPerson == null) return null;
            foundPerson.Email = updatePerson.Email;
            foundPerson.Address = updatePerson.Address;
            foundPerson.Gender = updatePerson.Gender;
            foundPerson.ReceiveNewsLetter = updatePerson.ReceiveNewsLetter;
            foundPerson.PersonName= updatePerson.PersonName;
            foundPerson.CountryId = updatePerson.CountryId;
            foundPerson.DateOfBirth = updatePerson.DateOfBirth;
            _db.SaveChanges();
            return foundPerson;
        }

        public Person? DeletePerson(Guid? guid)
        {
            Person? foundPerson = _db.People.FirstOrDefault((person) => person.PersonId == guid);
            if (foundPerson == null) return null;
            _db.People.Remove(foundPerson);
            _db.SaveChanges();
            return foundPerson;
        }
    }
}
