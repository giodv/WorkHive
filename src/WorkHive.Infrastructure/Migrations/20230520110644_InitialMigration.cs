using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkHive.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "WHEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    EventAttributes = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MaxGuest = table.Column<int>(type: "integer", nullable: true),
                    LocationAttributes = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WHEvents_WhUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "WhUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WHEventWHUser",
                columns: table => new
                {
                    GuestEventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WHEventWHUser", x => new { x.GuestEventsId, x.GuestsId });
                    table.ForeignKey(
                        name: "FK_WHEventWHUser_WHEvents_GuestEventsId",
                        column: x => x.GuestEventsId,
                        principalTable: "WHEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WHEventWHUser_WhUsers_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "WhUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WHEvents_OwnerId",
                table: "WHEvents",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WHEventWHUser_GuestsId",
                table: "WHEventWHUser",
                column: "GuestsId");

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
                name: "WHEvents");

            migrationBuilder.DropTable(
                name: "WhUsers");

            migrationBuilder.DropTable(
                name: "WhCompanies");
        }
    }
}
