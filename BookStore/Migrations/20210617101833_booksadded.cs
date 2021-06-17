using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class booksadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPhotos_Books_BooksId",
                table: "BookPhotos");

            migrationBuilder.DropIndex(
                name: "IX_BookPhotos_BooksId",
                table: "BookPhotos");

            migrationBuilder.DropColumn(
                name: "BooksId",
                table: "BookPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_BookPhotos_BookId",
                table: "BookPhotos",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPhotos_Books_BookId",
                table: "BookPhotos",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPhotos_Books_BookId",
                table: "BookPhotos");

            migrationBuilder.DropIndex(
                name: "IX_BookPhotos_BookId",
                table: "BookPhotos");

            migrationBuilder.AddColumn<int>(
                name: "BooksId",
                table: "BookPhotos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookPhotos_BooksId",
                table: "BookPhotos",
                column: "BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPhotos_Books_BooksId",
                table: "BookPhotos",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
