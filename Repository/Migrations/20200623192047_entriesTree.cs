using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class entriesTree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryId",
                table: "Positions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_EntryId",
                table: "Positions",
                column: "EntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Entries_EntryId",
                table: "Positions",
                column: "EntryId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Entries_EntryId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_EntryId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "EntryId",
                table: "Positions");
        }
    }
}
