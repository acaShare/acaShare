using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ChangedOnDeleteActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "File_EditRequest",
                table: "File");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialToUpdateId",
                table: "EditRequest",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest",
                column: "MaterialToUpdateId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "File_EditRequest",
                table: "File",
                column: "EditRequestId",
                principalTable: "EditRequest",
                principalColumn: "EditRequestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest");

            migrationBuilder.DropForeignKey(
                name: "File_EditRequest",
                table: "File");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialToUpdateId",
                table: "EditRequest",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "EditRequest_Material",
                table: "EditRequest",
                column: "MaterialToUpdateId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "File_EditRequest",
                table: "File",
                column: "EditRequestId",
                principalTable: "EditRequest",
                principalColumn: "EditRequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
