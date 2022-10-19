using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalPartnersTask.Migrations
{
    public partial class Sixth : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "TasksID",
                table: "Shift",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftID",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TasksID",
                table: "Shift",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
