using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialManagement.Data.Migrations
{
    public partial class global_variables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Users_DepositorId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_Users_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Guarantees_Users_GuarantorId",
                table: "Guarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_BorrowerId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_User_DepositorId",
                table: "Deposits",
                column: "DepositorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_User_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guarantees_User_GuarantorId",
                table: "Guarantees",
                column: "GuarantorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_User_BorrowerId",
                table: "Loans",
                column: "BorrowerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_User_DepositorId",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_Donations_User_DonorId",
                table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Guarantees_User_GuarantorId",
                table: "Guarantees");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_User_BorrowerId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Users_DepositorId",
                table: "Deposits",
                column: "DepositorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Users_DonorId",
                table: "Donations",
                column: "DonorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guarantees_Users_GuarantorId",
                table: "Guarantees",
                column: "GuarantorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_BorrowerId",
                table: "Loans",
                column: "BorrowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
