using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternLibrarie.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_copies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.BooksId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_fees_due = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_books_borrowed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "borrowedbooks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    overdue_fees = table.Column<int>(type: "int", nullable: false),
                    borrow_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowedbooks", x => x.id);
                    table.ForeignKey(
                        name: "FK_borrowedbooks_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "BooksId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_borrowedbooks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    SubscriptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subscription_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subscription_expiry_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.SubscriptionsId);
                    table.ForeignKey(
                        name: "FK_subscriptions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bills",
                columns: table => new
                {
                    BillsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrow_id = table.Column<int>(type: "int", nullable: false),
                    BorrowedBookid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bills", x => x.BillsId);
                    table.ForeignKey(
                        name: "FK_bills_borrowedbooks_BorrowedBookid",
                        column: x => x.BorrowedBookid,
                        principalTable: "borrowedbooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    borrow_id = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<int>(type: "int", nullable: false),
                    delivery_fee = table.Column<int>(type: "int", nullable: false),
                    adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowedBookid = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_bills_BorrowedBookid",
                table: "bills",
                column: "BorrowedBookid");

            migrationBuilder.CreateIndex(
                name: "IX_borrowedbooks_book_id",
                table: "borrowedbooks",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_borrowedbooks_user_id",
                table: "borrowedbooks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_BorrowedBookid",
                table: "deliveries",
                column: "BorrowedBookid");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_user_id",
                table: "subscriptions",
                column: "user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bills");

            migrationBuilder.DropTable(
                name: "deliveries");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "borrowedbooks");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
