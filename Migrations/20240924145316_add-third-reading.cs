using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApp.Migrations
{
    /// <inheritdoc />
    public partial class addthirdreading : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RepTest_RepRead3",
                table: "Calibrations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepTest_RepRead3",
                table: "Calibrations");
        }
    }
}
