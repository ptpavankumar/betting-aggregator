using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Betting_Aggregator.Business.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191218051900_addLeagueTable")]
    public class addEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "League",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false),
                Name = table.Column<string>(nullable: false),
                HasTable = table.Column<bool>(nullable: true),
                HasTopList = table.Column<bool>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_League", x => x.Id);
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "League");
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
#pragma warning restore 612, 618
        }
    }
}