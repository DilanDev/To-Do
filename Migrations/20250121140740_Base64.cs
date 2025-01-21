using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do.Migrations
{
    /// <inheritdoc />
    public partial class Base64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "DetalleTareas");

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "DetalleTareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64",
                table: "DetalleTareas");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "DetalleTareas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
