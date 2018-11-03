using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ReimplementedUniversityStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lesson_Department",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Subject_Lecturer",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Lesson_SectionOfSubject",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Semester_AcademicYear",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "Semester_SemesterNumber",
                table: "Semester");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Lecturer");

            migrationBuilder.DropTable(
                name: "SectionOfSubject");

            migrationBuilder.DropTable(
                name: "SemesterNumber");

            migrationBuilder.DropIndex(
                name: "IX_Semester_SemesterNumberId",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "Semester_Uniq_Idx",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_DepartmentId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_LecturerId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "Lesson_Uniq_Idx",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "AcademicYearId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "SemesterNumberId",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Lesson");

            migrationBuilder.RenameIndex(
                name: "IdentityUserId_Uniq_Idx",
                table: "User",
                newName: "UQ_AspNetUsers_IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "SectionOfSubjectId",
                table: "Lesson",
                newName: "SubjectDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SectionOfSubjectId",
                table: "Lesson",
                newName: "IX_Lesson_SubjectDepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "University",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Semester",
                unicode: false,
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "Department",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SubjectDepartment",
                columns: table => new
                {
                    SubjectDepartmentId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDepartment", x => x.SubjectDepartmentId);
                    table.ForeignKey(
                        name: "SubjectDepartment_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SubjectDepartment_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_UserType_Name",
                table: "UserType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_UserInUniversity",
                table: "UserInUniversity",
                columns: new[] { "UserId", "UniversityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_University_Name",
                table: "University",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Subject_Name",
                table: "Subject",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Semester_Number",
                table: "Semester",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_MaterialState_Name",
                table: "MaterialState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Lesson",
                table: "Lesson",
                columns: new[] { "SemesterId", "SubjectDepartmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Department_Name",
                table: "Department",
                column: "Name",
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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "SubjectDepartment");

            migrationBuilder.DropIndex(
                name: "UQ_UserType_Name",
                table: "UserType");

            migrationBuilder.DropIndex(
                name: "UQ_UserInUniversity",
                table: "UserInUniversity");

            migrationBuilder.DropIndex(
                name: "UQ_University_Name",
                table: "University");

            migrationBuilder.DropIndex(
                name: "UQ_Subject_Name",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "UQ_Semester_Number",
                table: "Semester");

            migrationBuilder.DropIndex(
                name: "UQ_MaterialState_Name",
                table: "MaterialState");

            migrationBuilder.DropIndex(
                name: "UQ_Lesson",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "UQ_Department_Name",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "University");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Semester");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "Department");

            migrationBuilder.RenameIndex(
                name: "UQ_AspNetUsers_IdentityUserId",
                table: "User",
                newName: "IdentityUserId_Uniq_Idx");

            migrationBuilder.RenameColumn(
                name: "SubjectDepartmentId",
                table: "Lesson",
                newName: "SectionOfSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesson_SubjectDepartmentId",
                table: "Lesson",
                newName: "IX_Lesson_SectionOfSubjectId");

            migrationBuilder.AddColumn<int>(
                name: "AcademicYearId",
                table: "Semester",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SemesterNumberId",
                table: "Semester",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Lesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Lesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    AcademicYearId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    YearFrom = table.Column<DateTime>(type: "date", nullable: false),
                    YearTo = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.AcademicYearId);
                });

            migrationBuilder.CreateTable(
                name: "Lecturer",
                columns: table => new
                {
                    LecturerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturer", x => x.LecturerId);
                });

            migrationBuilder.CreateTable(
                name: "SectionOfSubject",
                columns: table => new
                {
                    SectionOfSubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionOfSubject", x => x.SectionOfSubjectId);
                    table.ForeignKey(
                        name: "SectionOfSubject_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterNumber",
                columns: table => new
                {
                    SemesterNumberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterNumber", x => x.SemesterNumberId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semester_SemesterNumberId",
                table: "Semester",
                column: "SemesterNumberId");

            migrationBuilder.CreateIndex(
                name: "Semester_Uniq_Idx",
                table: "Semester",
                columns: new[] { "AcademicYearId", "SemesterNumberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_DepartmentId",
                table: "Lesson",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_LecturerId",
                table: "Lesson",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "Lesson_Uniq_Idx",
                table: "Lesson",
                columns: new[] { "SemesterId", "LecturerId", "DepartmentId", "SectionOfSubjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionOfSubject_SubjectId",
                table: "SectionOfSubject",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "Lesson_Department",
                table: "Lesson",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Subject_Lecturer",
                table: "Lesson",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SectionOfSubject",
                table: "Lesson",
                column: "SectionOfSubjectId",
                principalTable: "SectionOfSubject",
                principalColumn: "SectionOfSubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Semester_AcademicYear",
                table: "Semester",
                column: "AcademicYearId",
                principalTable: "AcademicYear",
                principalColumn: "AcademicYearId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Semester_SemesterNumber",
                table: "Semester",
                column: "SemesterNumberId",
                principalTable: "SemesterNumber",
                principalColumn: "SemesterNumberId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
