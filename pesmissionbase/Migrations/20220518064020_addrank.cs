using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pesmissionbase.Migrations
{
    public partial class addrank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ranl",
                table: "Grouping",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ranl",
                table: "Grouping");
        }
    }
}
