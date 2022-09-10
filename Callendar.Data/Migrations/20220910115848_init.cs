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
                    Collaborator1 = table.Column<string>(type: "text", nullable: true),
                    Collaborator2 = table.Column<string>(type: "text", nullable: true),
                    Collaborator3 = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Event_ID);
                    table.ForeignKey(
                        name: "FK_Event_User_Collaborator1",
                        column: x => x.Collaborator1,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_User_Collaborator2",
                        column: x => x.Collaborator2,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_User_Collaborator3",
                        column: x => x.Collaborator3,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_User_Owner_Username",
                        column: x => x.Owner_Username,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Collaborator1",
                table: "Event",
                column: "Collaborator1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Collaborator2",
                table: "Event",
                column: "Collaborator2");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Collaborator3",
                table: "Event",
                column: "Collaborator3");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Owner_Username",
                table: "Event",
                column: "Owner_Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
