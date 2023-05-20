using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHive.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMainEntitiesBetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "WHEvents",
                newName: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WHEvents_OwnerId",
                table: "WHEvents",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WHEvents_WhUsers_OwnerId",
                table: "WHEvents",
                column: "OwnerId",
                principalTable: "WhUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WHEvents_WhUsers_OwnerId",
                table: "WHEvents");

            migrationBuilder.DropIndex(
                name: "IX_WHEvents_OwnerId",
                table: "WHEvents");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "WHEvents",
                newName: "OrganizerId");
        }
    }
}
