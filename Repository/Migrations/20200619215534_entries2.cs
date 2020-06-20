using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class entries2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Entries_EntryId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EntryId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "Departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EntryId",
                table: "Departments",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Entries_EntryId",
                table: "Departments",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
