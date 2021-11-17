using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SanalBorsaAPI.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyPriceDolar = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    BuyPriceTL = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    MarketingValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketingSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExChangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    HighestPrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    LowestPrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExChangeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goldens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goldens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    HighestPrice = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    LowestPrice = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    MarketingSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrencies");

            migrationBuilder.DropTable(
                name: "ExChangeRates");

            migrationBuilder.DropTable(
                name: "Goldens");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
