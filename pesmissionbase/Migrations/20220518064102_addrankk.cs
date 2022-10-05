using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pesmissionbase.Migrations
{
    public partial class addrankk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ranl",
                table: "Grouping",
                newName: "Rank");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "Grouping",
                newName: "Ranl");
        }
    }
}
