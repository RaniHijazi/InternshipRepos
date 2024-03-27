using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternLibrarie.Migrations
{
    public partial class newmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliveries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowedBookid = table.Column<int>(type: "int", nullable: false),
                    adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrow_id = table.Column<int>(type: "int", nullable: false),
                    delivery_fee = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveries", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_deliveries_borrowedbooks_BorrowedBookid",
                        column: x => x.BorrowedBookid,
                        principalTable: "borrowedbooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_BorrowedBookid",
                table: "deliveries",
                column: "BorrowedBookid");
        }
    }
}
