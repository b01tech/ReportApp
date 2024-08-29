using ReportApp.Data;
using ReportApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ReportApp.Views;

public partial class SelectCustomerWindow : Window
{
    private readonly List<Customer> _customers;
    private readonly MainWindow _mainWindow;
    public SelectCustomerWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        _customers = LoadCustomers();
        LoadItens();
        _mainWindow = mainWindow;
    }


    public List<Customer> LoadCustomers()
    {
        using (var context = new AppDbContext())
        {
            return context.Customers.ToList();
        }
    }

    public void LoadItens()
    {
        foreach (var customer in _customers)
        {
            var item = new ListBoxItem
            {
                Content = customer.Name,
            };
            lbxCustomersList.Items.Add(item);
        }
    }

    private void btnSelectCustomer_Click(object sender, RoutedEventArgs e)
    {
       
        using (var context = new AppDbContext())
        {
            
        }
            

        this.Close();
    }
}

