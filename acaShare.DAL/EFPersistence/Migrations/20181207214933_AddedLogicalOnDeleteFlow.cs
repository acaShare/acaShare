using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedLogicalOnDeleteFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Comment_Material",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "Comment_User",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_User",
                table: "DeleteRequest");

            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest");

            migrationBuilder.DropForeignKey(
                name: "Department_University",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "EditRequest_User",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "Favorites_Material",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "Favorites_User",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "Subject_Semester",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "SubjectDepartment_Department",
                table: "SubjectDepartment");

            migrationBuilder.DropForeignKey(
                name: "SubjectDepartment_Subject",
                table: "SubjectDepartment");

            migrationBuilder.DropForeignKey(
                name: "UserUniversity_University",
                table: "UserInUniversity");

            migrationBuilder.DropForeignKey(
                name: "UserUniversity_User",
                table: "UserInUniversity");

            migrationBuilder.AddForeignKey(
                name: "Comment_Material",
                table: "Comment",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Comment_User",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_User",
                table: "DeleteRequest",
                column: "DeleterId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest",
                column: "MaterialToDeleteId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Department_University",
                table: "Department",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest",
                column: "MaterialToUpdateId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "EditRequest_User",
                table: "EditRequest",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Favorites_Material",
                table: "Favorites",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Favorites_User",
                table: "Favorites",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Subject_Semester",
                table: "Lesson",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "SemesterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson",
                column: "SubjectDepartmentId",
                principalTable: "SubjectDepartment",
                principalColumn: "SubjectDepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "SubjectDepartment_Department",
                table: "SubjectDepartment",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "SubjectDepartment_Subject",
                table: "SubjectDepartment",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_University",
                table: "UserInUniversity",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_User",
                table: "UserInUniversity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Comment_Material",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "Comment_User",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_User",
                table: "DeleteRequest");

            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest");

            migrationBuilder.DropForeignKey(
                name: "Department_University",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "EditRequest_User",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "Favorites_Material",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "Favorites_User",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "Subject_Semester",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "SubjectDepartment_Department",
                table: "SubjectDepartment");

            migrationBuilder.DropForeignKey(
                name: "SubjectDepartment_Subject",
                table: "SubjectDepartment");

            migrationBuilder.DropForeignKey(
                name: "UserUniversity_University",
                table: "UserInUniversity");

            migrationBuilder.DropForeignKey(
                name: "UserUniversity_User",
                table: "UserInUniversity");

            migrationBuilder.AddForeignKey(
                name: "Comment_Material",
                table: "Comment",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Comment_User",
                table: "Comment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_User",
                table: "DeleteRequest",
                column: "DeleterId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest",
                column: "MaterialToDeleteId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Department_University",
                table: "Department",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest",
                column: "MaterialToUpdateId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "EditRequest_User",
                table: "EditRequest",
                column: "UpdaterId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Favorites_Material",
                table: "Favorites",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Favorites_User",
                table: "Favorites",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Subject_Semester",
                table: "Lesson",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "SemesterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "Lesson_SubjectDepartment",
                table: "Lesson",
                column: "SubjectDepartmentId",
                principalTable: "SubjectDepartment",
                principalColumn: "SubjectDepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "SubjectDepartment_Department",
                table: "SubjectDepartment",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "SubjectDepartment_Subject",
                table: "SubjectDepartment",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_University",
                table: "UserInUniversity",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_User",
                table: "UserInUniversity",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
