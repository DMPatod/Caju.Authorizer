using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caju.Authorizer.Infrastructure.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class TransactionToTransactionIntentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionIntents_Transactions_TransactionId",
                table: "TransactionIntents");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "TransactionIntents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionIntents_Transactions_TransactionId",
                table: "TransactionIntents",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionIntents_Transactions_TransactionId",
                table: "TransactionIntents");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "TransactionIntents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionIntents_Transactions_TransactionId",
                table: "TransactionIntents",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
