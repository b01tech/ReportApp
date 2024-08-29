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

            var context = new AppDbContext();
            var seeder = new SeedingDbService(context);
            seeder.Seed();            
        }
    }

}
