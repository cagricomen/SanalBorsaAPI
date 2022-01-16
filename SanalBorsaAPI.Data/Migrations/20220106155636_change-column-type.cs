using Microsoft.EntityFrameworkCore.Migrations;

namespace SanalBorsaAPI.Data.Migrations
{
    public partial class changecolumntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "LowestPrice",
                table: "Stocks",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<float>(
                name: "LastPrice",
                table: "Stocks",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<float>(
                name: "HighestPrice",
                table: "Stocks",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)");

            migrationBuilder.AlterColumn<float>(
                name: "SalePrice",
                table: "Goldens",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "BuyPrice",
                table: "Goldens",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "SalePrice",
                table: "ExChangeRates",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "LowestPrice",
                table: "ExChangeRates",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "HighestPrice",
                table: "ExChangeRates",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "BuyPrice",
                table: "ExChangeRates",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "BuyPriceTL",
                table: "CryptoCurrencies",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "BuyPriceDolar",
                table: "CryptoCurrencies",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LowestPrice",
                table: "Stocks",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "LastPrice",
                table: "Stocks",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "HighestPrice",
                table: "Stocks",
                type: "decimal(20,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Goldens",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPrice",
                table: "Goldens",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "ExChangeRates",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "LowestPrice",
                table: "ExChangeRates",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "HighestPrice",
                table: "ExChangeRates",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPrice",
                table: "ExChangeRates",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPriceTL",
                table: "CryptoCurrencies",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyPriceDolar",
                table: "CryptoCurrencies",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
