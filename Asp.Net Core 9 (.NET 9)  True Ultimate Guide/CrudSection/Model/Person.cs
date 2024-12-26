namespace CrudSection.Model
{
    public class Person
    {
        public Person() { }
        public Person(Guid id, string name, int age, string job, Country country)
        {
            Id = id;
            Name = name;
            Age = age;
            Job = job;
            Country = country;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Job { get; set; }
        public Country Country { get; set; }
    }

}
