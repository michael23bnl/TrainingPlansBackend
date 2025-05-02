using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlans.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Plans_PlanEntityId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_PlanEntityId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "PlanEntityId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Exercises",
                table: "Plans",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exercises",
                table: "Plans");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanEntityId",
                table: "Exercises",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_PlanEntityId",
                table: "Exercises",
                column: "PlanEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Plans_PlanEntityId",
                table: "Exercises",
                column: "PlanEntityId",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
