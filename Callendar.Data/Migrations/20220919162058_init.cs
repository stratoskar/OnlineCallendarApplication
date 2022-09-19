using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Callendar.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Event_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date_Hour = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Owner_Username = table.Column<string>(type: "text", nullable: false),
                    Collaborators = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Event_ID);
                    table.ForeignKey(
                        name: "FK_Event_User_Owner_Username",
                        column: x => x.Owner_Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Notification_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Owner_Username = table.Column<string>(type: "text", nullable: false),
                    invited_person = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    attend_event = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Notification_ID);
                    table.ForeignKey(
                        name: "FK_Notification_User_Owner_Username",
                        column: x => x.Owner_Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Owner_Username",
                table: "Event",
                column: "Owner_Username");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Owner_Username",
                table: "Notification",
                column: "Owner_Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
