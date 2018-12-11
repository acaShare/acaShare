using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class MadeReasonUniqueAndAddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeleteRequest_DeleteReason_DeleteReasonId",
                table: "DeleteRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeleteReason",
                table: "DeleteReason");

            migrationBuilder.RenameTable(
                name: "DeleteReason",
                newName: "ChangeReason");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeReason",
                table: "ChangeReason",
                column: "ChangeReasonId");

            migrationBuilder.InsertData(
                table: "ChangeReason",
                columns: new[] { "ChangeReasonId", "Reason" },
                values: new object[,]
                {
                    { 1, "Nieodpowiednie treści" },
                    { 2, "Naruszenie praw własności" },
                    { 3, "Bezwartościowe informacje" },
                    { 4, "Inne" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ_ChangeReason_Reason",
                table: "ChangeReason",
                column: "Reason",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeleteRequest_ChangeReason_DeleteReasonId",
                table: "DeleteRequest",
                column: "DeleteReasonId",
                principalTable: "ChangeReason",
                principalColumn: "ChangeReasonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeleteRequest_ChangeReason_DeleteReasonId",
                table: "DeleteRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeReason",
                table: "ChangeReason");

            migrationBuilder.DropIndex(
                name: "UQ_ChangeReason_Reason",
                table: "ChangeReason");

            migrationBuilder.DeleteData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 4);

            migrationBuilder.RenameTable(
                name: "ChangeReason",
                newName: "DeleteReason");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeleteReason",
                table: "DeleteReason",
                column: "ChangeReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeleteRequest_DeleteReason_DeleteReasonId",
                table: "DeleteRequest",
                column: "DeleteReasonId",
                principalTable: "DeleteReason",
                principalColumn: "ChangeReasonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
