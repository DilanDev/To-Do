using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_do.Migrations
{
    /// <inheritdoc />
    public partial class DetalleTarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagenes_Tareas_TareaId",
                table: "Imagenes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Imagenes",
                table: "Imagenes");

            migrationBuilder.RenameTable(
                name: "Imagenes",
                newName: "DetalleTareas");

            migrationBuilder.RenameIndex(
                name: "IX_Imagenes_TareaId",
                table: "DetalleTareas",
                newName: "IX_DetalleTareas_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleTareas_Tareas_TareaId",
                table: "DetalleTareas",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleTareas_Tareas_TareaId",
                table: "DetalleTareas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleTareas",
                table: "DetalleTareas");

            migrationBuilder.RenameTable(
                name: "DetalleTareas",
                newName: "Imagenes");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleTareas_TareaId",
                table: "Imagenes",
                newName: "IX_Imagenes_TareaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imagenes",
                table: "Imagenes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagenes_Tareas_TareaId",
                table: "Imagenes",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
