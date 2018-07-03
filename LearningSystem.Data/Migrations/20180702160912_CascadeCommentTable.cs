using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class CascadeCommentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
