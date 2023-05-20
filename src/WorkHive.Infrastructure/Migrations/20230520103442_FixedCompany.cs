using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHive.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WHEventWHUser_WhUsers_GuestIdsId",
                table: "WHEventWHUser");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WhUsers");

            migrationBuilder.RenameColumn(
                name: "GuestIdsId",
                table: "WHEventWHUser",
                newName: "GuestsId");

            migrationBuilder.RenameIndex(
                name: "IX_WHEventWHUser_GuestIdsId",
                table: "WHEventWHUser",
                newName: "IX_WHEventWHUser_GuestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_WHEventWHUser_WhUsers_GuestsId",
                table: "WHEventWHUser",
                column: "GuestsId",
                principalTable: "WhUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WHEventWHUser_WhUsers_GuestsId",
                table: "WHEventWHUser");

            migrationBuilder.RenameColumn(
                name: "GuestsId",
                table: "WHEventWHUser",
                newName: "GuestIdsId");

            migrationBuilder.RenameIndex(
                name: "IX_WHEventWHUser_GuestsId",
                table: "WHEventWHUser",
                newName: "IX_WHEventWHUser_GuestIdsId");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "WhUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_WHEventWHUser_WhUsers_GuestIdsId",
                table: "WHEventWHUser",
                column: "GuestIdsId",
                principalTable: "WhUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
