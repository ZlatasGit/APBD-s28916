using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Version_Softwares_SoftwareId",
                table: "Version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Version",
                table: "Version");

            migrationBuilder.RenameTable(
                name: "Version",
                newName: "Versions");

            migrationBuilder.RenameIndex(
                name: "IX_Version_SoftwareId",
                table: "Versions",
                newName: "IX_Versions_SoftwareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Versions",
                table: "Versions",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Softwares_SoftwareId",
                table: "Versions",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Softwares_SoftwareId",
                table: "Versions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Versions",
                table: "Versions");

            migrationBuilder.RenameTable(
                name: "Versions",
                newName: "Version");

            migrationBuilder.RenameIndex(
                name: "IX_Versions_SoftwareId",
                table: "Version",
                newName: "IX_Version_SoftwareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Version",
                table: "Version",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Version_Softwares_SoftwareId",
                table: "Version",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
