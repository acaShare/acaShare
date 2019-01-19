using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class UserInUniversityChangedToUniversityMainModerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInUniversity");

            migrationBuilder.CreateTable(
                name: "UniversityMainModerator",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    UniversityId = table.Column<int>(nullable: false),
                    UserTypeTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityMainModerator", x => new { x.UserId, x.UniversityId });
                    table.ForeignKey(
                        name: "UserUniversity_University",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserUniversity_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityMainModerator_UserType_UserTypeTypeId",
                        column: x => x.UserTypeTypeId,
                        principalTable: "UserType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMainModerator_UniversityId",
                table: "UniversityMainModerator",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityMainModerator_UserTypeTypeId",
                table: "UniversityMainModerator",
                column: "UserTypeTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserInUniversity",
                table: "UniversityMainModerator",
                columns: new[] { "UserId", "UniversityId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniversityMainModerator");

            migrationBuilder.CreateTable(
                name: "UserInUniversity",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    UniversityId = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInUniversity", x => new { x.UserId, x.UniversityId });
                    table.ForeignKey(
                        name: "UserUniversity_UserType",
                        column: x => x.TypeId,
                        principalTable: "UserType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserUniversity_University",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "UserUniversity_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInUniversity_TypeId",
                table: "UserInUniversity",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInUniversity_UniversityId",
                table: "UserInUniversity",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "UQ_UserInUniversity",
                table: "UserInUniversity",
                columns: new[] { "UserId", "UniversityId" },
                unique: true);
        }
    }
}
