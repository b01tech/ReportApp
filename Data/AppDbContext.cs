using Microsoft.EntityFrameworkCore;
using ReportApp.Models;
using System.IO;
using System.Windows;

namespace ReportApp.Data;

public class AppDbContext : DbContext
{
    private string _databasePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Data\\AppDatabase.db"));

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Scale> Scales { get; set; }
    public DbSet<Weight> Weights { get; set; }
    public DbSet<Calibration> Calibrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Calibration>().OwnsOne(_ => _.RepTest);
        modelBuilder.Entity<Calibration>().OwnsOne(_ => _.MobTest);
        modelBuilder.Entity<Calibration>().OwnsOne(_ => _.EccTest);
        modelBuilder.Entity<Calibration>().OwnsMany(_ => _.WeightTest);
        modelBuilder.Entity<Calibration>().HasIndex(_ => _.ReportId).IsUnique();       
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlite($"Data Source={_databasePath}");
    }
}
