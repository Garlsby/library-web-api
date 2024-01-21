using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppApi.Migrations.StudentDb
{
    /// <inheritdoc />
    public partial class BookRemovingStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Student_StudentId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_StudentId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_StudentId1",
                table: "Books",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Student_StudentId1",
                table: "Books",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
