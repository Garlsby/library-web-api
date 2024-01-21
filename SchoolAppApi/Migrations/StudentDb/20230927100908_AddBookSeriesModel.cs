using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAppApi.Migrations.StudentDb
{
    /// <inheritdoc />
    public partial class AddBookSeriesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Books");
        }
    }
}
