using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PersonName = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", maxLength: 320, nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    CountryId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    ReceiveNewsLetter = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"), "United States" },
                    { new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"), "Canada" },
                    { new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"), "Australia" },
                    { new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"), "United Kingdom" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetter" },
                values: new object[,]
                {
                    { new Guid("a3d91284-dfc9-437e-9a7d-204f9fc037da"), "159 Aspen Way, Emerald City", new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"), new DateTime(1965, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "oliverwilson@example.com", "Male", "Oliver Wilson", false },
                    { new Guid("b234f654-a8b2-42b6-9f4a-90d58ebc3fcb"), "456 Oak Avenue, Metropolis", new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"), new DateTime(1990, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "janesmith@example.com", "Female", "Jane Smith", false },
                    { new Guid("c49cce9a-d6a3-4fbb-b1cf-5e9a84b92c4f"), "987 Cedar Court, Coast City", new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"), new DateTime(1982, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "sophiamartinez@example.com", "Female", "Sophia Martinez", false },
                    { new Guid("d2e4f962-2e17-4a09-9d90-e29f2d9158b7"), "147 Spruce Drive, Riverdale", new Guid("78e912ac-d73d-463b-9f39-37e53ef9e8b7"), new DateTime(1970, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "williamtaylor@example.com", "Male", "William Taylor", true },
                    { new Guid("d75f8a62-bfb5-4c45-a5b3-6ad0e88a2c0a"), "123 Elm Street, Springfield", new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"), new DateTime(1985, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "johndoe@example.com", "Male", "John Doe", true },
                    { new Guid("d912f360-e1a4-48fc-9461-fae8c22960f3"), "789 Maple Lane, Gotham", new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"), new DateTime(1978, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "samlee@example.com", "Male", "Samuel Lee", true },
                    { new Guid("e412c1c6-8823-44fc-a4bd-8e0df1fcaa9c"), "654 Pine Street, Central City", new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"), new DateTime(1995, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "michaelbrown@example.com", "Male", "Michael Brown", true },
                    { new Guid("f3d02915-b7a9-4f27-b8c6-a937c4c287c6"), "963 Redwood Road, Beacon Hills", new Guid("b78c0f21-3179-4976-9bbf-49d5a37b234e"), new DateTime(1992, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "charlottemoore@example.com", "Female", "Charlotte Moore", true },
                    { new Guid("f3d74215-57a6-4f02-85e7-9c4e3f4b7c30"), "852 Willow Street, Hill Valley", new Guid("56a49b0c-3e0d-4a4f-9e25-6a7c57d1f104"), new DateTime(1998, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "isabellajohnson@example.com", "Female", "Isabella Johnson", true },
                    { new Guid("f456789c-435a-4e4b-975c-d416ad8d35db"), "321 Birch Boulevard, Star City", new Guid("e71a8c23-97c5-47b5-9c72-bd2f517d0dc2"), new DateTime(2000, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "emilydavis@example.com", "Female", "Emily Davis", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
