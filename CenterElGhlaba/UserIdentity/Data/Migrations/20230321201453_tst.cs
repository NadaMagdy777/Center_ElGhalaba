using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Center_ElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class tst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelsSubject_Subjects_SubjectID",
                table: "LevelsSubject");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "LevelsSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LevelsSubject_Subjects_SubjectID",
                table: "LevelsSubject",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LevelsSubject_Subjects_SubjectID",
                table: "LevelsSubject");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "LevelsSubject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LevelsSubject_Subjects_SubjectID",
                table: "LevelsSubject",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");
        }
    }
}
