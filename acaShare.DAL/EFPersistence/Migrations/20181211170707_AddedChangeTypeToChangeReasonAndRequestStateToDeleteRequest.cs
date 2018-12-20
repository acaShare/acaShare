using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class AddedChangeTypeToChangeReasonAndRequestStateToDeleteRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestState",
                table: "DeleteRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChangeType",
                table: "ChangeReason",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 1,
                column: "ChangeType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 2,
                column: "ChangeType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 3,
                column: "ChangeType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ChangeReason",
                keyColumn: "ChangeReasonId",
                keyValue: 4,
                column: "ChangeType",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "DeleteRequest");

            migrationBuilder.DropColumn(
                name: "ChangeType",
                table: "ChangeReason");
        }
    }
}
