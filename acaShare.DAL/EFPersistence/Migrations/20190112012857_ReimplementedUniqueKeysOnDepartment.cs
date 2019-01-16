using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ReimplementedUniqueKeysOnDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Department_Name",
                table: "Department");

            migrationBuilder.CreateIndex(
                name: "UQ_Department_Abbreviation",
                table: "Department",
                columns: new[] { "Abbreviation", "UniversityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Department_Name",
                table: "Department",
                columns: new[] { "Name", "UniversityId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Department_Abbreviation",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "UQ_Department_Name",
                table: "Department");

            migrationBuilder.CreateIndex(
                name: "UQ_Department_Name",
                table: "Department",
                column: "Name",
                unique: true);
        }
    }
}
