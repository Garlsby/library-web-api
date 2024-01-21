using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppApi.Migrations.StudentDb
{
    /// <inheritdoc />
    public partial class nullableId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Student_StudentId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Student_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Student_StudentId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Student_StudentId",
                table: "Books",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
