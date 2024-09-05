﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReportApp.Data;

#nullable disable

namespace ReportApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CalibrationWeight", b =>
                {
                    b.Property<int>("CalibrationsCalibrationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WeightsWeightId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CalibrationsCalibrationId", "WeightsWeightId");

                    b.HasIndex("WeightsWeightId");

                    b.ToTable("CalibrationWeight");
                });

            modelBuilder.Entity("ReportApp.Models.Calibration", b =>
                {
                    b.Property<int>("CalibrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCal")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateReport")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manager")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ScaleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Technician")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CalibrationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ReportId")
                        .IsUnique();

                    b.HasIndex("ScaleId");

                    b.ToTable("Calibrations");
                });

            modelBuilder.Entity("ReportApp.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ReportApp.Models.Scale", b =>
                {
                    b.Property<int>("ScaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Capacity")
                        .HasColumnType("REAL");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("ResolutionD")
                        .HasColumnType("REAL");

                    b.Property<double>("ResolutionE")
                        .HasColumnType("REAL");

                    b.Property<int>("ScaleClass")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Unit")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScaleId");

                    b.ToTable("Scales");
                });

            modelBuilder.Entity("ReportApp.Models.Weight", b =>
                {
                    b.Property<int>("WeightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("NominalValue")
                        .HasColumnType("REAL");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WeightId");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("CalibrationWeight", b =>
                {
                    b.HasOne("ReportApp.Models.Calibration", null)
                        .WithMany()
                        .HasForeignKey("CalibrationsCalibrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportApp.Models.Weight", null)
                        .WithMany()
                        .HasForeignKey("WeightsWeightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReportApp.Models.Calibration", b =>
                {
                    b.HasOne("ReportApp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReportApp.Models.Scale", "Scale")
                        .WithMany()
                        .HasForeignKey("ScaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ReportApp.Models.EccTest", "EccTest", b1 =>
                        {
                            b1.Property<int>("CalibrationId")
                                .HasColumnType("INTEGER");

                            b1.Property<double?>("A")
                                .HasColumnType("REAL");

                            b1.Property<double?>("B")
                                .HasColumnType("REAL");

                            b1.Property<double?>("C")
                                .HasColumnType("REAL");

                            b1.Property<double?>("D")
                                .HasColumnType("REAL");

                            b1.Property<double?>("E")
                                .HasColumnType("REAL");

                            b1.Property<double?>("EccLoad")
                                .HasColumnType("REAL");

                            b1.Property<double?>("F")
                                .HasColumnType("REAL");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("CalibrationId");

                            b1.ToTable("Calibrations");

                            b1.WithOwner()
                                .HasForeignKey("CalibrationId");
                        });

                    b.OwnsOne("ReportApp.Models.MobTest", "MobTest", b1 =>
                        {
                            b1.Property<int>("CalibrationId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("MobAfter")
                                .HasColumnType("REAL");

                            b1.Property<double>("MobBefore")
                                .HasColumnType("REAL");

                            b1.Property<double>("MobLoad")
                                .HasColumnType("REAL");

                            b1.HasKey("CalibrationId");

                            b1.ToTable("Calibrations");

                            b1.WithOwner()
                                .HasForeignKey("CalibrationId");
                        });

                    b.OwnsOne("ReportApp.Models.RepTest", "RepTest", b1 =>
                        {
                            b1.Property<int>("CalibrationId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("RepMass")
                                .HasColumnType("REAL");

                            b1.Property<double>("RepRead1")
                                .HasColumnType("REAL");

                            b1.Property<double>("RepRead2")
                                .HasColumnType("REAL");

                            b1.HasKey("CalibrationId");

                            b1.ToTable("Calibrations");

                            b1.WithOwner()
                                .HasForeignKey("CalibrationId");
                        });

                    b.OwnsMany("ReportApp.Models.WeightTest", "WeightTest", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<int>("CalibrationId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("WLoad")
                                .HasColumnType("REAL");

                            b1.Property<double>("WRead")
                                .HasColumnType("REAL");

                            b1.HasKey("Id");

                            b1.HasIndex("CalibrationId");

                            b1.ToTable("WeightTest");

                            b1.WithOwner("Calibration")
                                .HasForeignKey("CalibrationId");

                            b1.Navigation("Calibration");
                        });

                    b.Navigation("Customer");

                    b.Navigation("EccTest")
                        .IsRequired();

                    b.Navigation("MobTest")
                        .IsRequired();

                    b.Navigation("RepTest")
                        .IsRequired();

                    b.Navigation("Scale");

                    b.Navigation("WeightTest");
                });
#pragma warning restore 612, 618
        }
    }
}
