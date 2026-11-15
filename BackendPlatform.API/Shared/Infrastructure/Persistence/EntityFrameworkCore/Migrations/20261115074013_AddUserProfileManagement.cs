using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nutritionist_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    specialty = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    experience_years = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_nutritionist_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "patient_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    first_name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    last_name = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    weight_kg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    height_meters = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_patient_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "goals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    patient_profile_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    target_weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    target_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_goals", x => x.id);
                    table.ForeignKey(
                        name: "f_k_goals_patient_profiles_patient_profile_id",
                        column: x => x.patient_profile_id,
                        principalTable: "patient_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    patient_profile_id = table.Column<int>(type: "int", nullable: false),
                    dietary_preferences = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    favorite_foods = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_preferences", x => x.id);
                    table.ForeignKey(
                        name: "f_k_preferences_patient_profiles_patient_profile_id",
                        column: x => x.patient_profile_id,
                        principalTable: "patient_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "restrictions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    patient_profile_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_restrictions", x => x.id);
                    table.ForeignKey(
                        name: "f_k_restrictions_patient_profiles_patient_profile_id",
                        column: x => x.patient_profile_id,
                        principalTable: "patient_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_goals_patient_profile_id",
                table: "goals",
                column: "patient_profile_id");

            migrationBuilder.CreateIndex(
                name: "i_x_nutritionist_profiles_user_id",
                table: "nutritionist_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_patient_profiles_user_id",
                table: "patient_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_preferences_patient_profile_id",
                table: "preferences",
                column: "patient_profile_id");

            migrationBuilder.CreateIndex(
                name: "i_x_restrictions_patient_profile_id",
                table: "restrictions",
                column: "patient_profile_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goals");

            migrationBuilder.DropTable(
                name: "nutritionist_profiles");

            migrationBuilder.DropTable(
                name: "preferences");

            migrationBuilder.DropTable(
                name: "restrictions");

            migrationBuilder.DropTable(
                name: "patient_profiles");
        }
    }
}
