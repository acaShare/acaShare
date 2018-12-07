using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class RemovedMaterialStateEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "MaterialState");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MaterialState",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ_MaterialState_Name",
                table: "MaterialState",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_MaterialState_Name",
                table: "MaterialState");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MaterialState");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "MaterialState",
                nullable: false,
                defaultValue: "");
        }
    }
}
