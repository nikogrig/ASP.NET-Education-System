using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class Secondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AutorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Articles",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AutorId",
                table: "Articles",
                newName: "IX_Articles_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AuthorId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Articles",
                newName: "AutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                newName: "IX_Articles_AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AutorId",
                table: "Articles",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
