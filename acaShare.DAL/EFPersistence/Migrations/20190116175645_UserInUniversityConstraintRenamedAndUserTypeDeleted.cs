using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class UserInUniversityConstraintRenamedAndUserTypeDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UserUniversity_University",
                table: "UniversityMainModerator");

            migrationBuilder.DropForeignKey(
                name: "UserUniversity_User",
                table: "UniversityMainModerator");

            migrationBuilder.DropForeignKey(
                name: "FK_UniversityMainModerator_UserType_UserTypeTypeId",
                table: "UniversityMainModerator");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropIndex(
                name: "IX_UniversityMainModerator_UserTypeTypeId",
                table: "UniversityMainModerator");

            migrationBuilder.DropColumn(
                name: "UserTypeTypeId",
                table: "UniversityMainModerator");

            migrationBuilder.RenameIndex(
                name: "UQ_UserInUniversity",
                table: "UniversityMainModerator",
                newName: "UQ_UniversityMainModerator");

            migrationBuilder.AddForeignKey(
                name: "UniversityMainModerator_University",
                table: "UniversityMainModerator",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UniversityMainModerator_User",
                table: "UniversityMainModerator",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UniversityMainModerator_University",
                table: "UniversityMainModerator");

            migrationBuilder.DropForeignKey(
                name: "UniversityMainModerator_User",
                table: "UniversityMainModerator");

            migrationBuilder.RenameIndex(
                name: "UQ_UniversityMainModerator",
                table: "UniversityMainModerator",
                newName: "UQ_UserInUniversity");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeTypeId",
                table: "UniversityMainModerator",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMainModerator_UserTypeTypeId",
                table: "UniversityMainModerator",
                column: "UserTypeTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserType_Name",
                table: "UserType",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_University",
                table: "UniversityMainModerator",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserUniversity_User",
                table: "UniversityMainModerator",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UniversityMainModerator_UserType_UserTypeTypeId",
                table: "UniversityMainModerator",
                column: "UserTypeTypeId",
                principalTable: "UserType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
