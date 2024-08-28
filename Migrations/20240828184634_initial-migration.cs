using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Scales",
                columns: table => new
                {
                    ScaleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagName = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    SerialNo = table.Column<string>(type: "TEXT", nullable: false),
                    ScaleClass = table.Column<int>(type: "INTEGER", nullable: false),
                    Capacity = table.Column<double>(type: "REAL", nullable: false),
                    ResolutionD = table.Column<double>(type: "REAL", nullable: false),
                    ResolutionE = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.ScaleId);
                });

            migrationBuilder.CreateTable(
                name: "Calibrations",
                columns: table => new
                {
                    CalibrationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportId = table.Column<string>(type: "TEXT", nullable: false),
                    Place = table.Column<string>(type: "TEXT", nullable: false),
                    DateCal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateReport = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Technician = table.Column<string>(type: "TEXT", nullable: false),
                    Manager = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    RepTest_RepMass = table.Column<double>(type: "REAL", nullable: false),
                    RepTest_RepRead1 = table.Column<double>(type: "REAL", nullable: false),
                    RepTest_RepRead2 = table.Column<double>(type: "REAL", nullable: false),
                    MobTest_MobBefore = table.Column<double>(type: "REAL", nullable: false),
                    MobTest_MobLoad = table.Column<double>(type: "REAL", nullable: false),
                    MobTest_MobAfter = table.Column<double>(type: "REAL", nullable: false),
                    EccTest_Type = table.Column<string>(type: "TEXT", nullable: false),
                    EccTest_EccLoad = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_A = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_B = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_C = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_D = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_E = table.Column<double>(type: "REAL", nullable: true),
                    EccTest_F = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations", x => x.CalibrationId);
                    table.ForeignKey(
                        name: "FK_Calibrations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calibrations_Scales_ScaleId",
                        column: x => x.ScaleId,
                        principalTable: "Scales",
                        principalColumn: "ScaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    WeightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagName = table.Column<string>(type: "TEXT", nullable: false),
                    NominalValue = table.Column<double>(type: "REAL", nullable: false),
                    CalibrationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.WeightId);
                    table.ForeignKey(
                        name: "FK_Weights_Calibrations_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "Calibrations",
                        principalColumn: "CalibrationId");
                });

            migrationBuilder.CreateTable(
                name: "WeightTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalibrationId = table.Column<int>(type: "INTEGER", nullable: false),
                    WLoad = table.Column<double>(type: "REAL", nullable: false),
                    WRead = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightTest_Calibrations_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "Calibrations",
                        principalColumn: "CalibrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_CustomerId",
                table: "Calibrations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Calibrations_ScaleId",
                table: "Calibrations",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_CalibrationId",
                table: "Weights",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightTest_CalibrationId",
                table: "WeightTest",
                column: "CalibrationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "WeightTest");

            migrationBuilder.DropTable(
                name: "Calibrations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Scales");
        }
    }
}
