using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Betting_Aggregator.Business.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191218051732_initialise")]
    public class initialise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK___EFMigrationsHistory", table: "__EFMigrationsHistory");

            migrationBuilder.AddColumn<int>("Id", "__EFMigrationsHistory", "int", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK___EFMigrationsHistory",
                table: "__EFMigrationsHistory",
                columns: new[] { "Id" }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK___EFMigrationsHistory", table: "__EFMigrationsHistory");
        }
    }
}