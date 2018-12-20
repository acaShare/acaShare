using Microsoft.EntityFrameworkCore.Migrations;

namespace acaShare.DAL.EFPersistence.Migrations
{
    public partial class TestRoleDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c44b3819-4779-4fc4-bb34-c8c3a53b288c", "211e2e67-458f-4b1f-a918-5452a50e044f", "Administrator", "ADMINISTRATOR" },
                    { "bd0553b5-bf3a-432a-a50c-1020ca9604ca", "bd8c88ab-39e4-4428-983e-585824af5213", "MainModerator", "MAINMODERATOR" },
                    { "7614cd25-8006-485d-8045-695cf6421cfd", "8ae3cd42-286c-4fe7-a1a0-805ad9cbbdf9", "Moderator", "MODERATOR" },
                    { "276988ca-214e-45db-b73b-b05a2a74c750", "81639d5c-f172-4a7f-866b-beed6b0901f3", "Member", "MEMBER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "276988ca-214e-45db-b73b-b05a2a74c750", "81639d5c-f172-4a7f-866b-beed6b0901f3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7614cd25-8006-485d-8045-695cf6421cfd", "8ae3cd42-286c-4fe7-a1a0-805ad9cbbdf9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "bd0553b5-bf3a-432a-a50c-1020ca9604ca", "bd8c88ab-39e4-4428-983e-585824af5213" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c44b3819-4779-4fc4-bb34-c8c3a53b288c", "211e2e67-458f-4b1f-a918-5452a50e044f" });

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
    }
}
