using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class OptedForStoringFilesInFileSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "FileFormat",
                table: "File",
                newName: "Format");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "File",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "Format",
                table: "File",
                newName: "FileFormat");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "File",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
