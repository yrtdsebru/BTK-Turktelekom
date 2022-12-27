using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApp.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dcc52f7-53fb-4ce6-b9a0-896cb0c12433", "ed194b3f-098f-4edf-a158-02f26315e39d", "user", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32e31edd-1edd-4cc0-b4fd-00c1e2975561", "9f9c650e-cb94-410f-932f-1ffec561df79", "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49de735e-adcd-44d1-8d41-1462b8471562", "08b08b1c-5ec3-44bb-9e0a-416abb20161a", "editor", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dcc52f7-53fb-4ce6-b9a0-896cb0c12433");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32e31edd-1edd-4cc0-b4fd-00c1e2975561");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49de735e-adcd-44d1-8d41-1462b8471562");
        }
    }
}
