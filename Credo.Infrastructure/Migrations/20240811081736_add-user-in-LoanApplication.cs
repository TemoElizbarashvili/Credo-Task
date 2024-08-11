using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduserinLoanApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LoanApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_UserId",
                table: "LoanApplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Users_UserId",
                table: "LoanApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Users_UserId",
                table: "LoanApplications");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_UserId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LoanApplications");
        }
    }
}
