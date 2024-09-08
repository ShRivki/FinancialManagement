using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManagement.Data.Migrations
{
    public partial class AAD_remainingAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RemainingAmount",
                table: "Loans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                table: "Loans");
        }
    }
}
