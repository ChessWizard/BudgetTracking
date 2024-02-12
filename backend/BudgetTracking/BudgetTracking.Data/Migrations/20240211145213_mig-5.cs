using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTracking.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentAccountId",
                table: "Expenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentAccountId",
                table: "Expenses",
                column: "PaymentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PaymentAccounts_PaymentAccountId",
                table: "Expenses",
                column: "PaymentAccountId",
                principalTable: "PaymentAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PaymentAccounts_PaymentAccountId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_PaymentAccountId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PaymentAccountId",
                table: "Expenses");
        }
    }
}
