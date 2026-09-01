using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mess_up_tags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietGoals_AspNetUsers_CreatedById",
                table: "DietGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_CreatedById",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_DietGoals_DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Meals");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Meals",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DietGoals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "DietGoals",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "DietGoals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MealTagEntity",
                columns: table => new
                {
                    MealId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTagEntity", x => new { x.MealId, x.Tag });
                    table.ForeignKey(
                        name: "FK_MealTagEntity_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietGoals_MealId",
                table: "DietGoals",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTagEntity_Tag",
                table: "MealTagEntity",
                column: "Tag");

            migrationBuilder.AddForeignKey(
                name: "FK_DietGoals_AspNetUsers_CreatedById",
                table: "DietGoals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DietGoals_Meals_MealId",
                table: "DietGoals",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_CreatedById",
                table: "Meals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietGoals_AspNetUsers_CreatedById",
                table: "DietGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_DietGoals_Meals_MealId",
                table: "DietGoals");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_CreatedById",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "MealTagEntity");

            migrationBuilder.DropIndex(
                name: "IX_DietGoals_MealId",
                table: "DietGoals");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "DietGoals");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Meals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DietGoalEntityId",
                table: "Meals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "Tags",
                table: "Meals",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "DietGoals",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "DietGoals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietGoalEntityId",
                table: "Meals",
                column: "DietGoalEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietGoals_AspNetUsers_CreatedById",
                table: "DietGoals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_CreatedById",
                table: "Meals",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_DietGoals_DietGoalEntityId",
                table: "Meals",
                column: "DietGoalEntityId",
                principalTable: "DietGoals",
                principalColumn: "Id");
        }
    }
}
