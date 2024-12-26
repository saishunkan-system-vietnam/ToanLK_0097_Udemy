using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class GetPerson_Proc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPerson = @"
            CREATE PROCEDURE `GetPerson` ()
            BEGIN
                SELECT * FROM PEOPLE;
            END;
            ";
            migrationBuilder.Sql(sp_GetAllPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPerson = @"
            DROP PROCEDURE `GetPerson`
            ";
            migrationBuilder.Sql(sp_GetAllPerson);
        }
    }
}
