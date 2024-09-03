using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApp.Migrations
{
    /// <inheritdoc />
    public partial class requiredfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_ReportId",
                table: "Calibrations",
                column: "ReportId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Calibrations_ReportId",
                table: "Calibrations");
        }
    }
}
