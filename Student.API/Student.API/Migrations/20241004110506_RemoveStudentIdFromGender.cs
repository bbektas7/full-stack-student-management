using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Student.API.Migrations
{
    public partial class RemoveStudentIdFromGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Gender tablosundan StudentId sütununu sil
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Gender");
        }

       
    }
}
