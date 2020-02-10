using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorIdentity.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05ebce49-0051-47fe-b382-65a74fea7995");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "129f8d3c-f076-41cc-a822-e9be998ebecf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "161a5b5a-6f6d-4725-a496-e585b9ffee43");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad233c1e-f7f2-44e7-be09-6f5533c28034", "2/10/2020 6:46:43 PM", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4c829ece-a79f-4d9a-a151-a558a1703b4f", "2/10/2020 6:46:43 PM", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "299badf3-e383-407c-a2d4-703638b717c0", "2/10/2020 6:46:43 PM", "user", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "299badf3-e383-407c-a2d4-703638b717c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c829ece-a79f-4d9a-a151-a558a1703b4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad233c1e-f7f2-44e7-be09-6f5533c28034");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "161a5b5a-6f6d-4725-a496-e585b9ffee43", "2/10/2020 4:26:26 AM", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "05ebce49-0051-47fe-b382-65a74fea7995", "2/10/2020 4:26:26 AM", "member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "129f8d3c-f076-41cc-a822-e9be998ebecf", "2/10/2020 4:26:26 AM", "family", "FAMILY" });
        }
    }
}
