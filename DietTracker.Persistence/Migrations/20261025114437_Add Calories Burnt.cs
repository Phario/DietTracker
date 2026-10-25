using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCaloriesBurnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaloriesBurnt",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesBurnt",
                table: "Workouts");
        }
    }
}
