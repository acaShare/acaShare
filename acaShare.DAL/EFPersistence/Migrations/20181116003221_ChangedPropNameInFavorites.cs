using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ChangedPropNameInFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Favorites",
                newName: "MaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_FileId",
                table: "Favorites",
                newName: "IX_Favorites_MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "Favorites",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_MaterialId",
                table: "Favorites",
                newName: "IX_Favorites_FileId");
        }
    }
}
