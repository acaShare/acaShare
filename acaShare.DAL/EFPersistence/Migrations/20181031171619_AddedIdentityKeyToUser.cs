using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedIdentityKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "User",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IdentityUserId_Uniq_Idx",
                table: "User",
                column: "IdentityUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IdentityUserId_Uniq_Idx",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "User");
        }
    }
}
