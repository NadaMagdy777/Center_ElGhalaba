using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CenterElGhlaba.Migrations
{
    /// <inheritdoc />
    public partial class setroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
