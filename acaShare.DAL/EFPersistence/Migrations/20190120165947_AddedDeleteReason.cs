using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedDeleteReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 2,
                column: "Reason",
                value: "Naruszenie praw autorskich");

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 4,
                column: "Reason",
                value: "Nieprawidłowa lokalizacja");

            migrationBuilder.InsertData(
                table: "ChangeReason",
                columns: new[] { "ChangeReasonId", "ChangeType", "Reason" },
                values: new object[] { 5, 1, "Inne" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 2,
                column: "Reason",
                value: "Naruszenie praw własności");

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 4,
                column: "Reason",
                value: "Inne");
        }
    }
}
