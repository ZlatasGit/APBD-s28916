using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class FixedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manies_Client_ClientId",
                table: "Manies");

            migrationBuilder.DropForeignKey(
                name: "FK_Manies_Ones_SubscriptionId",
                table: "Manies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Ones_SubscriptionId",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ones",
                table: "Ones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manies",
                table: "Manies");

            migrationBuilder.RenameTable(
                name: "Ones",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "Manies",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Manies_SubscriptionId",
                table: "Payment",
                newName: "IX_Payment_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Manies_ClientId",
                table: "Payment",
                newName: "IX_Payment_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "IdSubscription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "IdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Client_ClientId",
                table: "Payment",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Subscription_SubscriptionId",
                table: "Payment",
                column: "SubscriptionId",
                principalTable: "Subscription",
                principalColumn: "IdSubscription",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Subscription_SubscriptionId",
                table: "Sale",
                column: "SubscriptionId",
                principalTable: "Subscription",
                principalColumn: "IdSubscription",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Client_ClientId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Subscription_SubscriptionId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Subscription_SubscriptionId",
                table: "Sale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Ones");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Manies");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_SubscriptionId",
                table: "Manies",
                newName: "IX_Manies_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_ClientId",
                table: "Manies",
                newName: "IX_Manies_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ones",
                table: "Ones",
                column: "IdSubscription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manies",
                table: "Manies",
                column: "IdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Manies_Client_ClientId",
                table: "Manies",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manies_Ones_SubscriptionId",
                table: "Manies",
                column: "SubscriptionId",
                principalTable: "Ones",
                principalColumn: "IdSubscription",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Ones_SubscriptionId",
                table: "Sale",
                column: "SubscriptionId",
                principalTable: "Ones",
                principalColumn: "IdSubscription",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
