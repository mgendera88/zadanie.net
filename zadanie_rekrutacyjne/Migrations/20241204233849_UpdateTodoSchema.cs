using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zadanie_rekrutacyjne.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTodoSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "Todos",
                newName: "iscomplete");

            migrationBuilder.AddColumn<double>(
                name: "percent_done",
                table: "Todos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "percent_done",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "iscomplete",
                table: "Todos",
                newName: "isComplete");
        }
    }
}
