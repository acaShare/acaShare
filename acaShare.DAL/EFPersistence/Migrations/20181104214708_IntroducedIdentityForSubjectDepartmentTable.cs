using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class IntroducedIdentityForSubjectDepartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "UQ_Lesson",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectDepartment",
                table: "SubjectDepartment");

            migrationBuilder.DropColumn(
                name: "SubjectDepartmentId",
                table: "SubjectDepartment");

            migrationBuilder.AddColumn<int>(
               name: "SubjectDepartmentId",
               table: "SubjectDepartment",
               nullable: false)
               .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectDepartment",
                table: "SubjectDepartment",
                column: "SubjectDepartmentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Lesson",
                table: "Lesson",
                columns: new[] { "SemesterId", "SubjectDepartmentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson",
                column: "SubjectDepartmentId",
                principalTable: "SubjectDepartment",
                principalColumn: "SubjectDepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                 name: "Lesson_SubjectDepartment",
                 table: "Lesson");

            migrationBuilder.DropIndex(
                name: "UQ_Lesson",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectDepartment",
                table: "SubjectDepartment");

            migrationBuilder.DropColumn(
                name: "SubjectDepartmentId",
                table: "SubjectDepartment");

            migrationBuilder.AddColumn<int>(
               name: "SubjectDepartmentId",
               table: "SubjectDepartment",
               nullable: false)
               .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectDepartment",
                table: "SubjectDepartment",
                column: "SubjectDepartmentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Lesson",
                table: "Lesson",
                columns: new[] { "SemesterId", "SubjectDepartmentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson",
                column: "SubjectDepartmentId",
                principalTable: "SubjectDepartment",
                principalColumn: "SubjectDepartmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
