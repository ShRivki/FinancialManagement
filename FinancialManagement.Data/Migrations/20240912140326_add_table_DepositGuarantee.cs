using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManagement.Data.Migrations
{
    public partial class add_table_DepositGuarantee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepositGuarantee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositId = table.Column<int>(type: "int", nullable: false),
                    LoanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositGuarantee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepositGuarantee_Deposits_DepositId",
                        column: x => x.DepositId,
                        principalTable: "Deposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositGuarantee_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepositGuarantee_DepositId",
                table: "DepositGuarantee",
                column: "DepositId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositGuarantee_LoanId",
                table: "DepositGuarantee",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepositGuarantee");
        }
    }
}
