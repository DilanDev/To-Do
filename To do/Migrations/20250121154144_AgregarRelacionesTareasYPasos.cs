using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionesTareasYPasos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleTareas_TareaId",
                table: "DetalleTareas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DetalleTareas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "DetalleTareas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas",
                column: "TareaId");

            migrationBuilder.CreateTable(
                name: "PasoTarea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Completado = table.Column<bool>(type: "bit", nullable: false),
                    TareaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasoTarea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasoTarea_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasoTarea_TareaId",
                table: "PasoTarea",
                column: "TareaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasoTarea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Base64",
                table: "DetalleTareas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DetalleTareas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleTareas_TareaId",
                table: "DetalleTareas",
                column: "TareaId",
                unique: true);
        }
    }
}
