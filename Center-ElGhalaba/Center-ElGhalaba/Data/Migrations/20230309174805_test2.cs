using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Center_ElGhalaba.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentOrders_Payment_PaymentID",
                table: "StudentOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherPaymentMethod_Payment_PaymentID",
                table: "TeacherPaymentMethod");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_TeacherPaymentMethod_PaymentID",
                table: "TeacherPaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_StudentOrders_PaymentID",
                table: "StudentOrders");

            migrationBuilder.DropColumn(
                name: "PaymentID",
                table: "StudentOrders");

            migrationBuilder.RenameColumn(
                name: "Vlaue",
                table: "TeacherPaymentMethod",
                newName: "PaymentVlaue");

            migrationBuilder.AddColumn<string>(
                name: "PaymentName",
                table: "TeacherPaymentMethod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentName",
                table: "StudentOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentVlaue",
                table: "StudentOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentName",
                table: "TeacherPaymentMethod");

            migrationBuilder.DropColumn(
                name: "PaymentName",
                table: "StudentOrders");

            migrationBuilder.DropColumn(
                name: "PaymentVlaue",
                table: "StudentOrders");

            migrationBuilder.RenameColumn(
                name: "PaymentVlaue",
                table: "TeacherPaymentMethod",
                newName: "Vlaue");

            migrationBuilder.AddColumn<int>(
                name: "PaymentID",
                table: "StudentOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPaymentMethod_PaymentID",
                table: "TeacherPaymentMethod",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOrders_PaymentID",
                table: "StudentOrders",
                column: "PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentOrders_Payment_PaymentID",
                table: "StudentOrders",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherPaymentMethod_Payment_PaymentID",
                table: "TeacherPaymentMethod",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
