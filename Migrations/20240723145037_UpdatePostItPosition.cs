using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMC.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostItPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PostIts");

            migrationBuilder.AddColumn<int>(
                name: "PositionX",
                table: "PostIts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionY",
                table: "PostIts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "PostIts");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "PostIts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PostIts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
