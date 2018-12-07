using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedFileNameToFileAndChangedFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "File",
                newName: "FileData");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "File",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "File",
                newName: "File");
        }
    }
}
