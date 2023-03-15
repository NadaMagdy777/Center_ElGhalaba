using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CenterElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "0c81c7dd-34cf-46f0-937c-18b8576f4c7b");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "14c648c6-60d3-4334-9ed9-246db3f3461f");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8d0117b7-25d1-4b28-b52c-e798f0342bb2");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ea6ac1c-b245-490f-9df9-c8341b02a5cc", "d4d394b0-09ac-4435-871e-677ee762e72f", "Student", "STUDENT" },
                    { "632d2b3f-299b-4dc0-a382-25c331b20aeb", "25829d4c-e3f6-40c5-b173-8c6ea8cda00e", "Teacher", "TEACHER" },
                    { "a9b33c73-9b77-457d-bbd4-06e3116a0e1d", "290dcba0-1b7c-481e-ad73-6b83f7b74434", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "3ea6ac1c-b245-490f-9df9-c8341b02a5cc");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "632d2b3f-299b-4dc0-a382-25c331b20aeb");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "a9b33c73-9b77-457d-bbd4-06e3116a0e1d");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c81c7dd-34cf-46f0-937c-18b8576f4c7b", "2149e6e3-4976-41e9-a598-ca241179ee9a", "Admin", "ADMIN" },
                    { "14c648c6-60d3-4334-9ed9-246db3f3461f", "e5411f25-c768-4f52-8237-2735b8ae2e66", "Teacher", "TEACHER" },
                    { "8d0117b7-25d1-4b28-b52c-e798f0342bb2", "efb668fc-5d81-45cc-a56b-cf65c10b0c46", "Student", "STUDENT" }
                });
        }
    }
}
