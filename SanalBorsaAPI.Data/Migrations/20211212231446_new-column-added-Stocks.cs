using Microsoft.EntityFrameworkCore.Migrations;

namespace SanalBorsaAPI.Data.Migrations
{
    public partial class newcolumnaddedStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Stocks");
        }
    }
}
