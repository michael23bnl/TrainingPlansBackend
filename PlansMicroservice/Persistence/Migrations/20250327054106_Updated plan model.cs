using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlans.Migrations
{
    /// <inheritdoc />
    public partial class Updatedplanmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrepared",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "IsPrepared",
                table: "Exercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrepared",
                table: "Plans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrepared",
                table: "Exercises",
                type: "boolean",
                nullable: true);
        }
    }
}
