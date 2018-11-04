using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ChangedSemesterNumberLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Semester",
                unicode: false,
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Semester",
                unicode: false,
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3);
        }
    }
}
