using ReportApp.Data;
using ReportApp.Models;

namespace ReportApp.Views.ViewModels;

public class CalibrationViewModel
{
    public List<Calibration> Calibrations { get; set; }

    public CalibrationViewModel()
    {
        using (var context = new AppDbContext())
        {            
            context.Database.EnsureCreated();

            Calibrations = context.Calibrations.ToList();
        }
    }
}