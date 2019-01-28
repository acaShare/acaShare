using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class FixedTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UQ_University_Abbreviation",
                table: "University",
                newName: "UQ_University_Name");

            migrationBuilder.CreateIndex(
                name: "UQ_University_Abbreviation",
                table: "University",
                column: "Abbreviation",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_University_Abbreviation",
                table: "University");

            migrationBuilder.RenameIndex(
                name: "UQ_University_Name",
                table: "University",
                newName: "UQ_University_Abbreviation");
        }
    }
}
