using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalParkAPI_01.Migrations
{
    public partial class addrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Evaluation",
                table: "Trails",
                newName: "Elevation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Elevation",
                table: "Trails",
                newName: "Evaluation");
        }
    }
}
