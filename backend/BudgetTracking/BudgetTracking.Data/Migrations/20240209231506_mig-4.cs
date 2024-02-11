using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTracking.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccount_AspNetUsers_UserId",
                table: "PaymentAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentAccount",
                table: "PaymentAccount");

            migrationBuilder.RenameTable(
                name: "PaymentAccount",
                newName: "PaymentAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentAccount_UserId",
                table: "PaymentAccounts",
                newName: "IX_PaymentAccounts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentAccounts",
                table: "PaymentAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccounts_AspNetUsers_UserId",
                table: "PaymentAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentAccounts",
                table: "PaymentAccounts");

            migrationBuilder.RenameTable(
                name: "PaymentAccounts",
                newName: "PaymentAccount");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentAccounts_UserId",
                table: "PaymentAccount",
                newName: "IX_PaymentAccount_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentAccount",
                table: "PaymentAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccount_AspNetUsers_UserId",
                table: "PaymentAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
