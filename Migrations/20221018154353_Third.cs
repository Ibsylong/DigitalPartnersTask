using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPartnersTask.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Shift");

            migrationBuilder.AddColumn<int>(
                name: "ShiftID",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_ShiftID",
                table: "Task",
                column: "ShiftID");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Shift_ShiftID",
                table: "Task",
                column: "ShiftID",
                principalTable: "Shift",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Shift_ShiftID",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_ShiftID",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ShiftID",
                table: "Task");

            migrationBuilder.AddColumn<string>(
                name: "Tasks",
                table: "Shift",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
