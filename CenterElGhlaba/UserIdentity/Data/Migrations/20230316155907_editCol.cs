using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Center_ElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class editCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CoverPicture",
                table: "Lessons",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPicture",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Lessons");
        }
    }
}
