using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlans.Migrations
{
    /// <inheritdoc />
    public partial class changedplanmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Plans",
                newName: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Plans",
                newName: "Name");
        }
    }
}
