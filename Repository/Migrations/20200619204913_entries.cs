using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class entries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntryContinuationCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryContinuationCriterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntryReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrevId = table.Column<int>(nullable: true),
                    NextId = table.Column<int>(nullable: true),
                    OccurDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    ReasonId = table.Column<int>(nullable: true),
                    ContinuationCriteriaId = table.Column<int>(nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    PlannedStartDate = table.Column<DateTime>(nullable: true),
                    EstimatedResources = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_EntryContinuationCriterias_ContinuationCriteriaId",
                        column: x => x.ContinuationCriteriaId,
                        principalTable: "EntryContinuationCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Entries_NextId",
                        column: x => x.NextId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Entries_PrevId",
                        column: x => x.PrevId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_EntryReasons_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "EntryReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    EntryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EntryId",
                table: "Departments",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ContinuationCriteriaId",
                table: "Entries",
                column: "ContinuationCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_NextId",
                table: "Entries",
                column: "NextId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_PositionId",
                table: "Entries",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_PrevId",
                table: "Entries",
                column: "PrevId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ReasonId",
                table: "Entries",
                column: "ReasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "EntryContinuationCriterias");

            migrationBuilder.DropTable(
                name: "EntryReasons");
        }
    }
}
