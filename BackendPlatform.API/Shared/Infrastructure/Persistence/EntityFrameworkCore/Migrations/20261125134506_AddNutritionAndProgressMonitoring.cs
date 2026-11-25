using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddNutritionAndProgressMonitoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nutrition_plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    objective = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    start_date = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    end_date = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_nutrition_plans", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "progress_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    weight_kg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    body_fat_percentage = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: true),
                    waist_cm = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: true),
                    notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_progress_entries", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "meal_plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nutrition_plan_id = table.Column<int>(type: "int", nullable: false),
                    meal_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false),
                    calories = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_meal_plans", x => x.id);
                    table.ForeignKey(
                        name: "f_k_meal_plans__nutrition_plan_nutrition_plan_id",
                        column: x => x.nutrition_plan_id,
                        principalTable: "nutrition_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "food_recommendations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    meal_plan_id = table.Column<int>(type: "int", nullable: false),
                    food_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    portion = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false),
                    nutritional_value = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_food_recommendations", x => x.id);
                    table.ForeignKey(
                        name: "f_k_food_recommendations__meal_plan_meal_plan_id",
                        column: x => x.meal_plan_id,
                        principalTable: "meal_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_food_recommendations_meal_plan_id",
                table: "food_recommendations",
                column: "meal_plan_id");

            migrationBuilder.CreateIndex(
                name: "i_x_meal_plans_nutrition_plan_id",
                table: "meal_plans",
                column: "nutrition_plan_id");

            migrationBuilder.CreateIndex(
                name: "i_x_progress_entries_user_id_recorded_at",
                table: "progress_entries",
                columns: new[] { "user_id", "recorded_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_recommendations");

            migrationBuilder.DropTable(
                name: "progress_entries");

            migrationBuilder.DropTable(
                name: "meal_plans");

            migrationBuilder.DropTable(
                name: "nutrition_plans");
        }
    }
}
