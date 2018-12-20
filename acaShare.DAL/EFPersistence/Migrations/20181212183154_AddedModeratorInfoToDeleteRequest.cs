using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedModeratorInfoToDeleteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "DeleteRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeleteRequest_ModeratorId",
                table: "DeleteRequest",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeleteRequest_User",
                table: "DeleteRequest",
                column: "ModeratorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeleteRequest_User",
                table: "DeleteRequest");

            migrationBuilder.DropIndex(
                name: "IX_DeleteRequest_ModeratorId",
                table: "DeleteRequest");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "DeleteRequest");
        }
    }
}
