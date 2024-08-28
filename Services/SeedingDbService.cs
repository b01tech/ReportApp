using ReportApp.Data;
using ReportApp.Models;

namespace ReportApp.Services;

public class SeedingDbService
{
    private readonly AppDbContext _context;

    public SeedingDbService(AppDbContext context)
    {
        _context = context;
    }


    public void Seed()
    {
        _context.Database.EnsureCreated();

        if (_context.Weights.Any())
        {
            return;
        }

        var w1 = new Weight { WeightId = 1, TagName = "MP-01", NominalValue = 500.0 };
        var w2 = new Weight { WeightId = 2, TagName = "MP-02", NominalValue = 200.0 };
        var w3 = new Weight { WeightId = 3, TagName = "MP-03", NominalValue = 100.0 };
        var w4 = new Weight { WeightId = 4, TagName = "MP-04", NominalValue = 50.00 };

        _context.Weights.AddRange(w1, w2, w3, w4);
        _context.SaveChanges();
    }
}
