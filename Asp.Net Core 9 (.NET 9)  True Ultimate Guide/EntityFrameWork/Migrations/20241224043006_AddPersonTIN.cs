using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWork.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonTIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                type: "varchar(320)",
                maxLength: 320,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxIDnotation",
                table: "People",
                type: "varchar(8)",
                nullable: false,
                defaultValue: "ehehe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxIDnotation",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(320)",
                oldMaxLength: 320,
                oldNullable: true);
        }
    }
}
