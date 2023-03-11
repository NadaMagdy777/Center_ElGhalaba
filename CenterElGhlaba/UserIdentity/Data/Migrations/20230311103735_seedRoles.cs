using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CenterElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "50ade3aa-a8c9-442c-9438-23ed485ef0b6");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "b190af3e-6d80-4672-8a41-b06e4c7e3e83");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "c7b9dddf-d2df-4d8e-a053-91abc9f9f475");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e8dfdb8-f042-4982-a04f-bfc10992b027", "86ae3ab0-10af-4b26-9cf4-f348c2b95f92", "Student", "STUDENT" },
                    { "7df06f36-371c-46c1-b69e-cf71c63af2a3", "dcad198a-2d22-4345-a802-eacb0d7de633", "Teacher", "TEACHER" },
                    { "d345c42f-38bc-44cb-bbb2-86ec9aca23ed", "eea05618-09b5-4541-affc-5ef8a6f4cea4", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "3e8dfdb8-f042-4982-a04f-bfc10992b027");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "7df06f36-371c-46c1-b69e-cf71c63af2a3");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "d345c42f-38bc-44cb-bbb2-86ec9aca23ed");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "50ade3aa-a8c9-442c-9438-23ed485ef0b6", "00ac7828-26fd-47ef-b6d7-8a0b6a53ac88", "Student", "STUDENT" },
                    { "b190af3e-6d80-4672-8a41-b06e4c7e3e83", "52c44bc7-0828-4f38-ab82-10e93eba4c2e", "Teacher", "TEACHER" },
                    { "c7b9dddf-d2df-4d8e-a053-91abc9f9f475", "29d2c06e-06d8-4c5d-bdc5-bfea5a555d9b", "Admin", "ADMIN" }
                });
        }
    }
}
