using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Betting_Aggregator.Business.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191218051800_addCountryTable")]
    public class addCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Country",
            columns: table => new
            {
                ID = table.Column<int>(nullable: false),
                Name = table.Column<string>(nullable: false),
                CC = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Country", x => x.ID);
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Country");
        }
    }
}