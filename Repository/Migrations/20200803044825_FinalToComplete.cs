using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class FinalToComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinal",
                table: "Entries");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Entries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Entries");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinal",
                table: "Entries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
