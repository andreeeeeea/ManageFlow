using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageFlow.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskPriorityAndStatusEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, add the Priority column
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 1); // Default to Medium priority

            // Convert Status from string to integer using raw SQL
            // Map: "Not Started"/"Just Started" -> 0 (ToDo), "In Progress" -> 1, "Completed" -> 2, "On Hold" -> 3, "Cancelled" -> 4
            migrationBuilder.Sql(@"
                ALTER TABLE ""Tasks""
                ALTER COLUMN ""Status"" TYPE integer
                USING CASE
                    WHEN ""Status"" = 'Not Started' THEN 0
                    WHEN ""Status"" = 'Just Started' THEN 0
                    WHEN ""Status"" = 'ToDo' THEN 0
                    WHEN ""Status"" = 'In Progress' THEN 1
                    WHEN ""Status"" = 'InProgress' THEN 1
                    WHEN ""Status"" = 'Completed' THEN 2
                    WHEN ""Status"" = 'On Hold' THEN 3
                    WHEN ""Status"" = 'OnHold' THEN 3
                    WHEN ""Status"" = 'Cancelled' THEN 4
                    ELSE 0
                END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Convert Status back from integer to string
            migrationBuilder.Sql(@"
                ALTER TABLE ""Tasks""
                ALTER COLUMN ""Status"" TYPE text
                USING CASE
                    WHEN ""Status"" = 0 THEN 'ToDo'
                    WHEN ""Status"" = 1 THEN 'InProgress'
                    WHEN ""Status"" = 2 THEN 'Completed'
                    WHEN ""Status"" = 3 THEN 'OnHold'
                    WHEN ""Status"" = 4 THEN 'Cancelled'
                    ELSE 'ToDo'
                END;
            ");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");
        }
    }
}
