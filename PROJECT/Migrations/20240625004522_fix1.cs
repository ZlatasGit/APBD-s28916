using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AbstractClient_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_SoftwareSystems_SoftwareSystemId",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoftwareSystems",
                table: "SoftwareSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbstractClient",
                table: "AbstractClient");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "SoftwareSystems");

            migrationBuilder.RenameTable(
                name: "SoftwareSystems",
                newName: "Softwares");

            migrationBuilder.RenameTable(
                name: "AbstractClient",
                newName: "Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares",
                column: "IdSoftware");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "IdClient");

            migrationBuilder.CreateTable(
                name: "Version",
                columns: table => new
                {
                    IdVersion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Version", x => x.IdVersion);
                    table.ForeignKey(
                        name: "FK_Version_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Version_SoftwareId",
                table: "Version",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Softwares_SoftwareSystemId",
                table: "Contracts",
                column: "SoftwareSystemId",
                principalTable: "Softwares",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Softwares_SoftwareSystemId",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Softwares",
                newName: "SoftwareSystems");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "AbstractClient");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "SoftwareSystems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoftwareSystems",
                table: "SoftwareSystems",
                column: "IdSoftware");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbstractClient",
                table: "AbstractClient",
                column: "IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AbstractClient_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "AbstractClient",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_SoftwareSystems_SoftwareSystemId",
                table: "Contracts",
                column: "SoftwareSystemId",
                principalTable: "SoftwareSystems",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
