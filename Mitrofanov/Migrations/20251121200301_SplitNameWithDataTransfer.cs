using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioStatistic.Migrations
{
    /// <inheritdoc />
    public partial class SplitNameWithDataTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Engineers",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true); // Временно 

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Engineers",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true); // Временно 

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true); // Временно 

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true); // Временно 

            migrationBuilder.Sql("UPDATE \"Engineers\" SET \"FirstName\" = \"Name\", \"LastName\" = '' WHERE \"FirstName\" IS NULL");
            migrationBuilder.Sql("UPDATE \"Clients\" SET \"FirstName\" = \"Name\", \"LastName\" = '' WHERE \"FirstName\" IS NULL");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Engineers",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Engineers",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AboutHimself",
                table: "Engineers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Engineers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Engineers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Engineers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "AboutHimself",
                table: "Engineers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Engineers",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Clients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
