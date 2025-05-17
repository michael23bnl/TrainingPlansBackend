using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlans.Migrations
{
    /// <inheritdoc />
    public partial class changedexercisemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exercises");

            migrationBuilder.AddColumn<bool>(
                name: "IsPreMade",
                table: "Exercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreMade",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Exercises",
                type: "uuid",
                nullable: true);
        }
    }
}
