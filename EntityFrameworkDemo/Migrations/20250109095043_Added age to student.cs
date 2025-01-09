using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkDemo.Migrations
{
    /// <inheritdoc />
    public partial class Addedagetostudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Schools",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Schools");
        }
    }
}
