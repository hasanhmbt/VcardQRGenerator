using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VcardQRGenerator.Migrations
{
    /// <inheritdoc />
    public partial class qrurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "Vcards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "Vcards");
        }
    }
}
