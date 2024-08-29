using ReportApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ReportApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void btnWeights_Click(object sender, RoutedEventArgs e)
    {
        var weightWindow = new SelectWeightWindow(this);
        weightWindow.ShowDialog();
    }

    public void AddWeight(List<string> weights)
    {
        spWeights.Children.Clear();
        foreach (var weight in weights)
        {
            var label = new Label
            {
                Content = weight
            };
            spWeights.Children.Add(label);
        }

    }
}
