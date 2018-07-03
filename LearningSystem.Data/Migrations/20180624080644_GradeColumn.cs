﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningSystem.Data.Migrations
{
    public partial class GradeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "StudentCourses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentCourses");
        }
    }
}
