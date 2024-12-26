using EntityFrameWork.Entities;

namespace EntityFrameWork.Service
{
    public interface IPersonService
    {
        public List<Person> GetAllPerson();
        public Person? GetById(Guid? id);
        public Person AddPerson(Person person);
        public Person? UpdatePerson(Person updatePerson);
        public Person? DeletePerson(Guid? guid);
    }
}
