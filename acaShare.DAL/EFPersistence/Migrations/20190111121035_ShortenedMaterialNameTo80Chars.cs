using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class ShortenedMaterialNameTo80Chars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Material",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51825ef9-7c53-41c0-88a2-53768f3fdb4b", "c6f86064-ac00-4fa1-9e51-9c8c1ea6fce9", "Administrator", "ADMINISTRATOR" },
                    { "b1f6687c-5bff-425f-af5c-5341b44c64c0", "178c3ef7-ad05-44a6-93c4-a28c9bf72571", "MainModerator", "MAINMODERATOR" },
                    { "2eb6a235-de89-4d75-9ef7-3f44b352fb58", "4b976fc9-50fa-4c24-9544-d7fad5f7ab5a", "Moderator", "MODERATOR" },
                    { "b64c4ab9-b764-49ba-aff9-8d5cf733751c", "4284daa1-3427-48d3-8732-885b0c63addf", "Member", "MEMBER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2eb6a235-de89-4d75-9ef7-3f44b352fb58", "4b976fc9-50fa-4c24-9544-d7fad5f7ab5a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "51825ef9-7c53-41c0-88a2-53768f3fdb4b", "c6f86064-ac00-4fa1-9e51-9c8c1ea6fce9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b1f6687c-5bff-425f-af5c-5341b44c64c0", "178c3ef7-ad05-44a6-93c4-a28c9bf72571" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b64c4ab9-b764-49ba-aff9-8d5cf733751c", "4284daa1-3427-48d3-8732-885b0c63addf" });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Material",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 80);
        }
    }
}
