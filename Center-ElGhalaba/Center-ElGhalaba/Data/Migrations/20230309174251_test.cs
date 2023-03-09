using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Center_ElGhalaba.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "TeacherLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLogs_TeacherID",
                table: "TeacherLogs",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherLogs_Teachers_TeacherID",
                table: "TeacherLogs",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherLogs_Teachers_TeacherID",
                table: "TeacherLogs");

            migrationBuilder.DropIndex(
                name: "IX_TeacherLogs_TeacherID",
                table: "TeacherLogs");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "TeacherLogs");
        }
    }
}
