using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VcardQRGenerator.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Vcards",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Vcards",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Vcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Vcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Vcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Vcards");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Vcards");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Vcards");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Vcards");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Vcards",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Vcards",
                newName: "Address");
        }
    }
}
