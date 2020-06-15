using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class position : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Vendors_VendorId",
                table: "Catalog");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Catalog",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Vendors_VendorId",
                table: "Catalog",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Vendors_VendorId",
                table: "Catalog");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Catalog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Vendors_VendorId",
                table: "Catalog",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
