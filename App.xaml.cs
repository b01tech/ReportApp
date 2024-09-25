using ReportApp.Data;
using ReportApp.Services;
using System.Windows;

namespace ReportApp
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Console.WriteLine("Sistema iniciado.");

            var context = new AppDbContext();
            var seeder = new SeedingDbService(context);
            seeder.Seed();
            Console.WriteLine("Seed feito");

            //ExcelImporter ex = new ExcelImporter();
            //ex.ImportExcelToDb("C:\\Users\\Bruno\\Desktop\\Pasta1.xlsx");
        }
    }

}
