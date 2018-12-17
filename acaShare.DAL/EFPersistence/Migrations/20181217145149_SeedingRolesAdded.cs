using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class SeedingRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b4a68bbe-ab6c-4b5f-861c-90b55feb5888", "2abe2c82-00dc-44a1-878f-44b6a41f0c96", "Administrator", "ADMINISTRATOR" },
                    { "d436cead-c56c-48ab-94a0-4cbd4ee5f566", "d9f419fb-bec6-40f7-89b5-2cb7aed43978", "MainModerator", "MAINMODERATOR" },
                    { "65463026-d015-4611-9133-ad03e19e193e", "860ae1a3-83ac-4637-b9eb-450c31b250e9", "Moderator", "MODERATOR" },
                    { "b6c7c4e8-7436-4015-85c1-d66c7b4b2f3b", "56f150c1-1a71-4323-ab61-689bb22c716a", "Member", "MEMBER" },
                    { "6e42f725-10bc-4213-86cf-ea0e3fb042ab", "22044b24-ebdc-49a1-816b-3e1efa1832bc", "aaa", "AAA" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "65463026-d015-4611-9133-ad03e19e193e", "860ae1a3-83ac-4637-b9eb-450c31b250e9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6e42f725-10bc-4213-86cf-ea0e3fb042ab", "22044b24-ebdc-49a1-816b-3e1efa1832bc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b4a68bbe-ab6c-4b5f-861c-90b55feb5888", "2abe2c82-00dc-44a1-878f-44b6a41f0c96" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b6c7c4e8-7436-4015-85c1-d66c7b4b2f3b", "56f150c1-1a71-4323-ab61-689bb22c716a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d436cead-c56c-48ab-94a0-4cbd4ee5f566", "d9f419fb-bec6-40f7-89b5-2cb7aed43978" });
        }
    }
}
