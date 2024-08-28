using Microsoft.EntityFrameworkCore;
using ReportApp.Models;

namespace ReportApp.Data;

internal class AppDbContext : DbContext
{
    private string _databasePath = "DataSource = Data\\AppDatabase.db";
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

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_databasePath);
    }
}
