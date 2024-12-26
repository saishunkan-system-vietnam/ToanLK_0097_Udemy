using EntityFrameWork.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace EntityFrameWork.Context
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(){}
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options){}
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>();
            modelBuilder.Entity<Person>();
            
            string countries = System.IO.File.ReadAllText("DummyData/country.json");
            List<Country> country = JsonSerializer.Deserialize<List<Country>>(countries)??new List<Country>();
            string people = System.IO.File.ReadAllText("DummyData/person.json");
            List<Person> person = JsonSerializer.Deserialize<List<Person>>(people) ?? new List<Person>();

            modelBuilder.Entity<Country>().HasData(country);
            modelBuilder.Entity<Person>().HasData(person);

            modelBuilder.Entity<Person>().Property((person) => person.TIN)
                .HasColumnName("TaxIDnotation")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("ehehe");

            modelBuilder.Entity<Person>().HasCheckConstraint("CHK_TIN", "len(TaxIDNotation) > 8");

            // table relation
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne<Country>(c => c.Country)
                .WithMany(p => p.Person)
                .HasForeignKey(p => p.CountryId);
            });
        }

        public List<Person> sp_GetAllPerson()
        {
            return People.FromSqlRaw("call Getperson").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@PersonId",person.PersonId),
                 new MySqlParameter("@PersonName",person.PersonName),
                  new MySqlParameter("@Email",person.Email),
                   new MySqlParameter("@DateOfBirth",person.DateOfBirth),
                    new MySqlParameter("@Gender",person.Gender),
                     new MySqlParameter("@CountryId",person.CountryId),
                      new MySqlParameter("@Address",person.Address),
                       new MySqlParameter("@ReceiveNewsLetter",person.ReceiveNewsLetter)
            };

            return Database.ExecuteSqlRaw("CALL InsertPerson(@PersonId,@PersonName,@Email,@DateOfBirth,@Gender,@CountryId,@Address,@ReceiveNewsLetter)", parameters);

        }
    }
}
