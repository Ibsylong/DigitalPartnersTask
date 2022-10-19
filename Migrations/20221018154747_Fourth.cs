using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPartnersTask.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tasks",
                table: "Shift");

            migrationBuilder.AddColumn<int>(
                name: "ShiftID",
                table: "Task",
                type: "int",
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
    }
}
