using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksWebApi.Migrations
{
    public partial class AddeUserIdToTaskTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ToDos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_CreatedById",
                table: "ToDos",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Users_CreatedById",
                table: "ToDos",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Users_CreatedById",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_CreatedById",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ToDos");
        }
    }
}
