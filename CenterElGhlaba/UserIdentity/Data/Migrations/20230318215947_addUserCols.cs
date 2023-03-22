using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Center_ElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class addUserCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                schema: "Security",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LessonComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinDate",
                schema: "Security",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LessonComments");
        }
    }
}
