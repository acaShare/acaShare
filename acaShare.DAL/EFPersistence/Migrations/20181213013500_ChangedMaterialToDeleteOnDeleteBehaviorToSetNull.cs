using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ChangedMaterialToDeleteOnDeleteBehaviorToSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialToDeleteId",
                table: "DeleteRequest",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest",
                column: "MaterialToDeleteId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialToDeleteId",
                table: "DeleteRequest",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "DeleteRequest_Material",
                table: "DeleteRequest",
                column: "MaterialToDeleteId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
