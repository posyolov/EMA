using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class entries_no_linkedlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_NextId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_PrevId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_NextId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_PrevId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "NextId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "PrevId",
                table: "Entries");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDateTime",
                table: "Entries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ChangeUserId",
                table: "Entries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinal",
                table: "Entries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Entries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ChangeUserId",
                table: "Entries",
                column: "ChangeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ParentId",
                table: "Entries",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_ChangeUserId",
                table: "Entries",
                column: "ChangeUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_ParentId",
                table: "Entries",
                column: "ParentId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Users_ChangeUserId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Entries_ParentId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ChangeUserId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ParentId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "ChangeDateTime",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ChangeUserId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "IsFinal",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "NextId",
                table: "Entries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrevId",
                table: "Entries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_NextId",
                table: "Entries",
                column: "NextId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_PrevId",
                table: "Entries",
                column: "PrevId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_NextId",
                table: "Entries",
                column: "NextId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Entries_PrevId",
                table: "Entries",
                column: "PrevId",
                principalTable: "Entries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
