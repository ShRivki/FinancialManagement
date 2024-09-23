using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManagement.Data.Migrations
{
    public partial class Balance_currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalFundBalanceEUR",
                table: "GlobalVariables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFundBalanceGBP",
                table: "GlobalVariables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFundBalanceILS",
                table: "GlobalVariables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalFundBalanceUSD",
                table: "GlobalVariables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalFundBalanceEUR",
                table: "GlobalVariables");

            migrationBuilder.DropColumn(
                name: "TotalFundBalanceGBP",
                table: "GlobalVariables");

            migrationBuilder.DropColumn(
                name: "TotalFundBalanceILS",
                table: "GlobalVariables");

            migrationBuilder.DropColumn(
                name: "TotalFundBalanceUSD",
                table: "GlobalVariables");
        }
    }
}
