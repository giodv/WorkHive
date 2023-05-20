using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHive.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuestIds",
                table: "WHEvents",
                newName: "LocationAttributes");

            migrationBuilder.RenameColumn(
                name: "EventType",
                table: "WHEvents",
                newName: "EventAttributes");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WHEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "WHEvents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WHEvents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WhCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    WhCompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WhUsers_WhCompanies_WhCompanyId",
                        column: x => x.WhCompanyId,
                        principalTable: "WhCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WHEventWHUser",
                columns: table => new
                {
                    GuestEventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestIdsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHEventWHUser", x => new { x.GuestEventsId, x.GuestIdsId });
                    table.ForeignKey(
                        name: "FK_WHEventWHUser_WHEvents_GuestEventsId",
                        column: x => x.GuestEventsId,
                        principalTable: "WHEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WHEventWHUser_WhUsers_GuestIdsId",
                        column: x => x.GuestIdsId,
                        principalTable: "WhUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WHEventWHUser_GuestIdsId",
                table: "WHEventWHUser",
                column: "GuestIdsId");

            migrationBuilder.CreateIndex(
                name: "IX_WhUsers_WhCompanyId",
                table: "WhUsers",
                column: "WhCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WHEventWHUser");

            migrationBuilder.DropTable(
                name: "WhUsers");

            migrationBuilder.DropTable(
                name: "WhCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WHEvents");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WHEvents");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WHEvents");

            migrationBuilder.RenameColumn(
                name: "LocationAttributes",
                table: "WHEvents",
                newName: "GuestIds");

            migrationBuilder.RenameColumn(
                name: "EventAttributes",
                table: "WHEvents",
                newName: "EventType");
        }
    }
}
