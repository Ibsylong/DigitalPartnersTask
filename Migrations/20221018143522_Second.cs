using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPartnersTask.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftWorkerID",
                table: "Shift",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TasksID",
                table: "Shift",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftWorkerID",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "TasksID",
                table: "Shift");
        }
    }
}
