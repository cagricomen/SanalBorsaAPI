using Microsoft.EntityFrameworkCore.Migrations;

namespace SanalBorsaAPI.Data.Migrations
{
    public partial class newcolumnaddedExChangeRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "ExChangeRates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "ExChangeRates");
        }
    }
}
