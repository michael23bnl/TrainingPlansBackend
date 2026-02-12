using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlans.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPlans_Plans_PlanId",
                table: "CustomPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExerciseEntity_Exercises_ExerciseId",
                table: "PlanExerciseEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExerciseEntity_Plans_PlanId",
                table: "PlanExerciseEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomPlans",
                table: "CustomPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanExerciseEntity",
                table: "PlanExerciseEntity");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Plans");

            migrationBuilder.RenameTable(
                name: "PlanExerciseEntity",
                newName: "PlanExercises");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "CustomPlans",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExerciseEntity_ExerciseId",
                table: "PlanExercises",
                newName: "IX_PlanExercises_ExerciseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Plans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Plans",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomPlans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CustomPlans",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourcePlanId",
                table: "CustomPlans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PlanExercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "PlanExercises",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "PlanExercises",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomPlans",
                table: "CustomPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanExercises",
                table: "PlanExercises",
                columns: new[] { "PlanId", "ExerciseId" });

            migrationBuilder.CreateTable(
                name: "CustomPlanExercises",
                columns: table => new
                {
                    PlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Sets = table.Column<int>(type: "integer", nullable: true),
                    Reps = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomPlanExercises", x => new { x.PlanId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_CustomPlanExercises_CustomPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "CustomPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomPlanExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomPlans_SourcePlanId",
                table: "CustomPlans",
                column: "SourcePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomPlanExercises_ExerciseId",
                table: "CustomPlanExercises",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPlans_Plans_SourcePlanId",
                table: "CustomPlans",
                column: "SourcePlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseId",
                table: "PlanExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExercises_Plans_PlanId",
                table: "PlanExercises",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomPlans_Plans_SourcePlanId",
                table: "CustomPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExercises_Exercises_ExerciseId",
                table: "PlanExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanExercises_Plans_PlanId",
                table: "PlanExercises");

            migrationBuilder.DropTable(
                name: "CustomPlanExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomPlans",
                table: "CustomPlans");

            migrationBuilder.DropIndex(
                name: "IX_CustomPlans_SourcePlanId",
                table: "CustomPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanExercises",
                table: "PlanExercises");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomPlans");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CustomPlans");

            migrationBuilder.DropColumn(
                name: "SourcePlanId",
                table: "CustomPlans");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "PlanExercises");

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "PlanExercises");

            migrationBuilder.DropColumn(
                name: "Sets",
                table: "PlanExercises");

            migrationBuilder.RenameTable(
                name: "PlanExercises",
                newName: "PlanExerciseEntity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomPlans",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_PlanExercises_ExerciseId",
                table: "PlanExerciseEntity",
                newName: "IX_PlanExerciseEntity_ExerciseId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Plans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomPlans",
                table: "CustomPlans",
                columns: new[] { "PlanId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanExerciseEntity",
                table: "PlanExerciseEntity",
                columns: new[] { "PlanId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomPlans_Plans_PlanId",
                table: "CustomPlans",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExerciseEntity_Exercises_ExerciseId",
                table: "PlanExerciseEntity",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanExerciseEntity_Plans_PlanId",
                table: "PlanExerciseEntity",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
