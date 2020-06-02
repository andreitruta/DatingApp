using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class AddedLockerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lockers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LockerId = table.Column<int>(nullable: false),
                    LockerUserId = table.Column<int>(nullable: false),
                    LockerVacant = table.Column<bool>(nullable: false),
                    LockerBusy = table.Column<bool>(nullable: false),
                    LockerCheckIn = table.Column<DateTime>(nullable: false),
                    LockerCheckOut = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lockers");
        }
    }
}
