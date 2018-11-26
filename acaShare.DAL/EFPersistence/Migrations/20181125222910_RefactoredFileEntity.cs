using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class RefactoredFileEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Format",
                table: "File",
                newName: "ContentType");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "File",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "File",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "File",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "File",
                newName: "Format");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "File",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "File",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
