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
            return context.Customers
                .OrderBy(_ => _.Name)
                .ToList();
        }
    }

    public void LoadItens()
    {
        foreach (var customer in _customers)
        {
            var item = new ListBoxItem
            {
                Content = customer.Name,
                Tag = customer
            };
            lbxCustomersList.Items.Add(item);
        }
    }

    private void btnSelectCustomer_Click(object sender, RoutedEventArgs e)
    {
        var selectListboxItem = lbxCustomersList.SelectedItem as ListBoxItem;
        if (selectListboxItem != null)
        {
            var selectCustomer = selectListboxItem.Tag as Customer;

            using (var context = new AppDbContext())
            {
                _mainWindow.txtCustomerName.Text = selectCustomer.Name;
                _mainWindow.txtAddress.Text = selectCustomer.Address;
                _mainWindow.txtCity.Text = selectCustomer.City;
                _mainWindow.cbState.Text = selectCustomer.State;
            }
        } 

        this.Close();
    }
}

