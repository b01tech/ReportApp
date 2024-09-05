using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApp.Migrations
{
    /// <inheritdoc />
    public partial class relationcalibrationweightajust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weights_Calibrations_CalibrationId",
                table: "Weights");

            migrationBuilder.DropIndex(
                name: "IX_Weights_CalibrationId",
                table: "Weights");

            migrationBuilder.DropColumn(
                name: "CalibrationId",
                table: "Weights");

            migrationBuilder.CreateTable(
                name: "CalibrationWeight",
                columns: table => new
                {
                    CalibrationsCalibrationId = table.Column<int>(type: "INTEGER", nullable: false),
                    WeightsWeightId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationWeight", x => new { x.CalibrationsCalibrationId, x.WeightsWeightId });
                    table.ForeignKey(
                        name: "FK_CalibrationWeight_Calibrations_CalibrationsCalibrationId",
                        column: x => x.CalibrationsCalibrationId,
                        principalTable: "Calibrations",
                        principalColumn: "CalibrationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalibrationWeight_Weights_WeightsWeightId",
                        column: x => x.WeightsWeightId,
                        principalTable: "Weights",
                        principalColumn: "WeightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationWeight_WeightsWeightId",
                table: "CalibrationWeight",
                column: "WeightsWeightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationWeight");

            migrationBuilder.AddColumn<int>(
                name: "CalibrationId",
                table: "Weights",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weights_CalibrationId",
                table: "Weights",
                column: "CalibrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_Calibrations_CalibrationId",
                table: "Weights",
                column: "CalibrationId",
                principalTable: "Calibrations",
                principalColumn: "CalibrationId");
        }
    }
}
