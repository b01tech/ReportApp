using ReportApp.Data;
using ReportApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ReportApp.Views;


public partial class SelectWeightWindow : Window
{
    private readonly List<Weight> _weights;
    private readonly MainWindow _mainWindow;
    public SelectWeightWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        _weights = LoadWeight();
        LoadCheckbox();

    }


    private List<Weight> LoadWeight()
    {
        using (var context = new AppDbContext())
        {
            return context.Weights.ToList();
        }

    }

    private void LoadCheckbox()
    {
        spWeightsList.Children.Clear(); 

        foreach (var w in _weights)
        {
            var checkbox = new CheckBox();

            if (w.NominalValue >= 1)
            {
                checkbox.Content = $"{w.TagName} - {w.NominalValue} kg";
                checkbox.Tag = w;
            }
            else
            {
                checkbox.Content = $"{w.TagName} - {w.NominalValue * 1000} g";
                checkbox.Tag = w;
            }

            spWeightsList.Children.Add(checkbox); 
        }
    }

    private void btnAddWeights_Click(object sender, RoutedEventArgs e)
    {
        var selectWeightList = spWeightsList.Children
            .OfType<CheckBox>()
            .Where(cb => cb.IsChecked == true)
            .Select(c => c.Tag as Weight).ToList();
    

        _mainWindow.AddWeight(selectWeightList);
        this.Close();
    }
}
