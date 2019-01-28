using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ReimplementedUniversityTreeStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "SubjectDepartment");

            migrationBuilder.DropIndex(
                name: "UQ_Subject_Name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "UQ_Lesson",
                table: "Lesson");

            migrationBuilder.RenameIndex(
                name: "UQ_University_Name",
                table: "University",
                newName: "UQ_University_Abbreviation");

            migrationBuilder.RenameColumn(
                name: "SubjectDepartmentId",
                table: "Lesson",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SubjectDepartmentId",
                table: "Lesson",
                newName: "IX_Lesson_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Lesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UQ_Subject_Name_Abbreviation",
                table: "Subject",
                columns: new[] { "Name", "Abbreviation" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_DepartmentId",
                table: "Lesson",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UQ_Lesson",
                table: "Lesson",
                columns: new[] { "SemesterId", "SubjectId", "DepartmentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Lesson_Department",
                table: "Lesson",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Lesson_Subject",
                table: "Lesson",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lesson_Department",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Lesson_Subject",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "UQ_Subject_Name_Abbreviation",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_DepartmentId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "UQ_Lesson",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Lesson");

            migrationBuilder.RenameIndex(
                name: "UQ_University_Abbreviation",
                table: "University",
                newName: "UQ_University_Name");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Lesson",
                newName: "SubjectDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SubjectId",
                table: "Lesson",
                newName: "IX_Lesson_SubjectDepartmentId");

            migrationBuilder.CreateTable(
                name: "SubjectDepartment",
                columns: table => new
                {
                    SubjectDepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDepartment", x => x.SubjectDepartmentId);
                    table.ForeignKey(
                        name: "SubjectDepartment_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "SubjectDepartment_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Subject_Name",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Lesson",
                table: "Lesson",
                columns: new[] { "SemesterId", "SubjectDepartmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDepartment_DepartmentId",
                table: "SubjectDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UQ_SubjectDepartment",
                table: "SubjectDepartment",
                columns: new[] { "SubjectId", "DepartmentId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson",
                column: "SubjectDepartmentId",
                principalTable: "SubjectDepartment",
                principalColumn: "SubjectDepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
