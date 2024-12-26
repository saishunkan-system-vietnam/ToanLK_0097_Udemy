using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class InsertPerson_Proc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"
            CREATE PROCEDURE `InsertPerson` (
                PersonId char(36),
                PersonName nvarchar(60),
                Email nvarchar(100),
                DateTime datetime,
                Gender nvarchar(10),
                CountryId char(36),
                Address nvarchar(200),
                ReceiveNewsLetter bit)
            BEGIN
                insert into people
                values(PersonId,PersonName,Email,DateTime,Gender,CountryId,Address,ReceiveNewsLetter);
            END;
            ";
            migrationBuilder.Sql(sp_InsertPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"
            DROP PROCEDURE `InsertPerson`
            ";
            migrationBuilder.Sql(sp_InsertPerson);
        }
    }
}
