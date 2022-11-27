using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class startPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "/images/products/default.jpg"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "..."),
                    AtCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, 17000m, "HP ZBook" },
                    { 2, 3500m, "Airpods" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "ProductName" },
                values: new object[,]
                {
                    { 3, "JBL kulak üstü kulaklıkları. 16 saat pil ömrü.", "/images/products/jbl.jpg", 1000m, "JBL" },
                    { 4, "Samsung Laptop Touch your dreams", "/images/products/samsung.jpg", 15000m, "Samsung Laptop" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
