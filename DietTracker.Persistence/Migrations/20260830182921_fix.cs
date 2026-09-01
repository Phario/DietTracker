using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_UpdatedById",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UpdatedById",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Meals");

            migrationBuilder.AddColumn<Guid>(
                name: "DietGoalEntityId",
                table: "Meals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIngredient",
                table: "Meals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DietGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietGoals_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietGoalEntityId",
                table: "Meals",
                column: "DietGoalEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DietGoals_CreatedById",
                table: "DietGoals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_DietGoals_DietGoalEntityId",
                table: "Meals",
                column: "DietGoalEntityId",
                principalTable: "DietGoals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_DietGoals_DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "DietGoals");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Meals_DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "DietGoalEntityId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "IsIngredient",
                table: "Meals");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Meals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UpdatedById",
                table: "Meals",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_UpdatedById",
                table: "Meals",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
