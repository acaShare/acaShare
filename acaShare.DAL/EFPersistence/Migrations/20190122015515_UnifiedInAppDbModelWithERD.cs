using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class UnifiedInAppDbModelWithERD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Comment",
                newName: "CreateDate");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Semester",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notification",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MaterialState",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Material",
                maxLength: 10000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "NewName",
                table: "EditRequest",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewDescription",
                table: "EditRequest",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_User_Username",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Comment",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Semester",
                unicode: false,
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notification",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MaterialState",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Material",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10000);

            migrationBuilder.AlterColumn<string>(
                name: "NewName",
                table: "EditRequest",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NewDescription",
                table: "EditRequest",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10000,
                oldNullable: true);
        }
    }
}
