﻿using Microsoft.EntityFrameworkCore;
using ReportApp.Data;
using ReportApp.Models;
using ReportApp.Models.Enums;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReportApp.Views;

public partial class MainWindow : Window
{
    private SortedSet<string> _users = new SortedSet<string>
    {
        "Bruno Marçau", "Roni Cleber", "Marcelo Moreira", "Dimas dos Reis", "Paulo", "Israel Peres","Welington"
    };
    private SortedSet<string> _managers = new SortedSet<string>
    {
        "Roni Cleber"
    };
    private List<string> _status = new List<string> { "Aprovado", "Reprovado" };
    private List<string> _scaleClass = new List<string>
    { "Não se aplica","Classe I", "Classe II", "Classe III", "Classe IV" };
    private List<string> _unit = new List<string> { "mg", "g", "kg" };
    private SortedSet<string> _states = new SortedSet<string>
{
    "AC", // Acre
    "AL", // Alagoas
    "AP", // Amapá
    "AM", // Amazonas
    "BA", // Bahia
    "CE", // Ceará
    "DF", // Distrito Federal
    "ES", // Espírito Santo
    "GO", // Goiás
    "MA", // Maranhão
    "MT", // Mato Grosso
    "MS", // Mato Grosso do Sul
    "MG", // Minas Gerais
    "PA", // Pará
    "PB", // Paraíba
    "PR", // Paraná
    "PE", // Pernambuco
    "PI", // Piauí
    "RJ", // Rio de Janeiro
    "RN", // Rio Grande do Norte
    "RS", // Rio Grande do Sul
    "RO", // Rondônia
    "RR", // Roraima
    "SC", // Santa Catarina
    "SP", // São Paulo
    "SE", // Sergipe
    "TO"  // Tocantins
};
    private SortedSet<string> _ecctypes = new SortedSet<string> { "Não se aplica", "Industrial", "Rodoviária" };
    private static readonly Regex _regex = new Regex("[^0-9,.]+");


    public MainWindow()
    {
        InitializeComponent();
        LoadUsers();
        LoadManagers();
        LoadStatusReport();
        LoadScaleClasses();
        LoadUnits();
        LoadStates();
        LoadEccTypes();
    }

    private void LoadEccTypes()
    {
        foreach (var ecct in _ecctypes)
        {
            cbEccTestType.Items.Add(ecct);
        }
    }

    private void btnWeights_Click(object sender, RoutedEventArgs e)
    {
        var weightWindow = new SelectWeightWindow(this);
        weightWindow.ShowDialog();
    }

    public void AddWeight(List<string> weights)
    {
        spWeightsList.Children.Clear();
        foreach (var weight in weights)
        {
            var label = new Label
            {
                Content = weight
            };
            spWeightsList.Children.Add(label);
        }
    }

    public void LoadUsers()
    {
        foreach (var user in _users)
        {
            var cbItem = new ComboBoxItem
            {
                Content = user
            };
            cbTechnician.Items.Add(cbItem);
        }
    }
    public void LoadManagers()
    {
        foreach (var m in _managers)
        {
            var cbItem = new ComboBoxItem
            {
                Content = m
            };
            cbManager.Items.Add(cbItem);
        }
    }

    public void LoadStatusReport()
    {
        foreach (var st in _status)
        {
            var cbItem = new ComboBoxItem
            {
                Content = st
            };
            cbStatusReport.Items.Add(cbItem);
        }
    }
    public void LoadScaleClasses()
    {
        foreach (var s in _scaleClass)
        {
            var cbItem = new ComboBoxItem
            {
                Content = s
            };
            cbScaleClass.Items.Add(cbItem);
        }
    }
    public void LoadUnits()
    {
        foreach (var u in _unit)
        {
            var cbItem = new ComboBoxItem
            {
                Content = u
            };
            cbUnit.Items.Add(cbItem);
        }
    }
    public void LoadStates()
    {
        foreach (var s in _states)
        {
            var cbItem = new ComboBoxItem
            {
                Content = s
            };
            cbState.Items.Add(cbItem);
        }
    }

    private void cbEccTestType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cbEccTestType.SelectedValue?.ToString() == "Não se aplica")
        {
            txtEccLoad.IsEnabled = false;
            txtEccLoad.Text = string.Empty;
            txtEccReadA.IsEnabled = false;
            txtEccReadA.Text = string.Empty;
            txtEccReadB.IsEnabled = false;
            txtEccReadB.Text = string.Empty;
            txtEccReadC.IsEnabled = false;
            txtEccReadC.Text = string.Empty;
            txtEccReadD.IsEnabled = false;
            txtEccReadD.Text = string.Empty;
            txtEccReadE.IsEnabled = false;
            txtEccReadE.Text = string.Empty;
            txtEccReadF.IsEnabled = false;
            txtEccReadF.Text = string.Empty;
        }
        else
        {
            txtEccLoad.IsEnabled = true;
            txtEccReadA.IsEnabled = true;
            txtEccReadB.IsEnabled = true;
            txtEccReadC.IsEnabled = true;
            txtEccReadD.IsEnabled = true;
            txtEccReadE.IsEnabled = true;
            txtEccReadF.IsEnabled = true;

        }
    }

    private void OnlyNumbers(object sender, TextCompositionEventArgs e)
    {
        e.Handled = _regex.IsMatch(e.Text);
    }
    private bool IsValidNumber(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        string normalizedText = text.Replace(',', '.');

        return double.TryParse(normalizedText, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
    }

    private void resetNotValidNumberInput(object sender, RoutedEventArgs e)
    {
        var textbox = sender as TextBox;
        if (textbox is not null)
        {

            if (!IsValidNumber(textbox.Text.Trim()))
            {
                textbox.Text = string.Empty;

            }

        }
    }

    private void btnNew_Click(object sender, RoutedEventArgs e)
    {
        ClearAllInputs(spWeightTest);
        ClearAllInputs(spMobTest);
        ClearAllInputs(spRepTest);
        ClearAllInputs(spEccTest);
        ClearAllInputs(spScaleInfo);
    }
    private void ClearAllInputs(Panel panel)
    {
        foreach (var child in panel.Children)
        {
            if (child is TextBox textBox)
            {
                textBox.Text = string.Empty;
            }
            else if (child is ComboBox comboBox)
            {
                comboBox.SelectedIndex = -1;
            }
            else if (child is Panel childPanel)
            {
                ClearAllInputs(childPanel);
            }
        }
    }

    private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
    {
        var selectCustomer = new SelectCustomerWindow(this);
        selectCustomer.ShowDialog();

    }

    private void calcError(object sender, RoutedEventArgs e)
    {
        resetNotValidNumberInput(sender, e);

        var weightRead = sender as TextBox;

        if (!string.IsNullOrEmpty(weightRead?.Text))
        {
            string weightLoadName = weightRead.Name.Replace("Read", "Load");
            var weightLoad = (TextBox)FindName(weightLoadName);
            string weightErrorName = weightRead.Name.Replace("Read", "Error");
            var weightError = (TextBox)FindName(weightErrorName);

            if (!string.IsNullOrEmpty(weightLoad.Text))
            {
                if (double.TryParse(weightLoad.Text, out double loadValue) && double.TryParse(weightRead.Text, out double readValue))
                {
                    double error = loadValue - readValue;
                    weightError.Text = error.ToString("F4");
                }
                else
                {
                    weightError.Text = string.Empty;
                }

            }

        }
    }

    private void btnPrint_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        if (mainWindow != null)
        {
            SaveNewReport(mainWindow);
        }
    }

    private void SaveNewReport(MainWindow mainWindow)
    {
        using (var context = new AppDbContext())
        {
            try
            {
                Customer customer = new Customer();
                if (context.Customers.Any(c => c.Name == mainWindow.txtCustomerName.Text))
                {
                    customer = context.Customers.FirstOrDefault(c => c.Name == mainWindow.txtCustomerName.Text);
                }
                else
                {
                    customer = new Customer
                    {
                        Name = mainWindow.txtCustomerName.Text,
                        Address = mainWindow.txtAddress.Text,
                        City = mainWindow.txtCity.Text,
                        State = mainWindow.cbState.Text
                    };
                }

                var scale = new Scale
                {
                    TagName = mainWindow.txtTagName.Text,
                    SerialNo = mainWindow.txtSerialNo.Text,
                    ResolutionD = double.Parse(mainWindow.txtResolutionD.Text),
                    ResolutionE = double.Parse(mainWindow.txtResolutionE.Text),
                    Capacity = double.Parse(mainWindow.txtCapacity.Text),
                    Model = mainWindow.txtModel.Text,
                    Unit = Enum.TryParse<Unit>(mainWindow.cbUnit.Text, out var u) ? u : Unit.Kg, 
                    ScaleClass = Enum.TryParse<ScaleClass>(mainWindow.cbScaleClass.Text, out var cl) ? cl : ScaleClass.ClassIII,
                };

                var repTest = new RepTest
                {
                    RepMass = double.Parse(mainWindow.txtRepMassApply.Text),
                    RepRead1 = double.Parse(mainWindow.txtRepRead1.Text),
                    RepRead2 = double.Parse(mainWindow.txtRepRead2.Text)
                };

                var mobTest = new MobTest
                {
                    MobBefore = double.Parse(mainWindow.txtMobReadBefore.Text),
                    MobLoad = double.Parse(mainWindow.txtMobOverLoad.Text),
                    MobAfter = double.Parse(mainWindow.txtMobReadAfter.Text),
                };

                var eccTest = new EccTest
                {
                    Type = mainWindow.cbEccTestType.Text,
                    EccLoad = TryParseDouble(mainWindow.txtEccLoad?.Text),
                    A = TryParseDouble(mainWindow.txtEccReadA?.Text),
                    B = TryParseDouble(mainWindow.txtEccReadB?.Text),
                    C = TryParseDouble(mainWindow.txtEccReadC?.Text),
                    D = TryParseDouble(mainWindow.txtEccReadD?.Text),
                    E = TryParseDouble(mainWindow.txtEccReadE?.Text),
                    F = TryParseDouble(mainWindow.txtEccReadF?.Text),
                };

                var weightTestList = new List<WeightTest>();                
                var weights = new List<Weight>();

                var cal = new Calibration
                {
                    ReportId = mainWindow.txtReportId.Text,
                    Place = mainWindow.txtPlace.Text,
                    Technician = mainWindow.cbTechnician.Text,
                    Manager = mainWindow.cbManager.Text,
                    Customer = customer,
                    RepTest = repTest,
                    MobTest = mobTest,
                    EccTest = eccTest,
                    DateCal = DateTime.Now,
                    DateReport = DateTime.Now,
                    Weights = weights,
                    WeightTest = weightTestList,
                    Status = Enum.TryParse<ReportStatus>(mainWindow.cbStatusReport.Text, out var status) ? status: ReportStatus.Aprovado,
                    Scale = scale


                };
                context.Calibrations.Add(cal);
                context.SaveChanges();
                MessageBox.Show($"Certificado {mainWindow.txtReportId.Text} salvo com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o relatório: Campos inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }

    private double? TryParseDouble(string text)
    {
        return double.TryParse(text, out double value) ? value : (double?)null;
    }

    private void btnSearchId_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        if (mainWindow != null)
        {
            LoadReport(mainWindow);
        }
    }

    private void LoadReport(MainWindow mainwindow)
    {
        if (string.IsNullOrEmpty(mainwindow.txtReportId.Text))
        {
            return;
        }
        else
        {
            using (var context = new AppDbContext())
            {
                var report = context.Calibrations
                    .Include(c => c.Customer)
                    .Include(c => c.Scale)
                    .Include(c => c.Weights)
                    .Include(c => c.WeightTest)
                    .FirstOrDefault(c => c.ReportId == mainwindow.txtReportId.Text);

                if (report is null)
                {
                    return;
                }
                else
                {
                    MessageBox.Show($"Informações do certificado {mainwindow.txtReportId.Text} carregadas");
                    mainwindow.txtCustomerName.Text = report.Customer.Name;
                    mainwindow.txtAddress.Text = report.Customer.Address;
                    mainwindow.txtCity.Text = report.Customer.City;
                    mainwindow.cbState.Text = report.Customer.State;

                    mainwindow.txtModel.Text = report.Scale.Model;
                    mainwindow.txtSerialNo.Text = report.Scale.SerialNo;
                    mainwindow.txtTagName.Text = report.Scale.TagName;
                    mainwindow.txtCapacity.Text = report.Scale.Capacity.ToString();
                    mainwindow.txtResolutionD.Text = report.Scale.ResolutionD.ToString();
                    mainwindow.txtResolutionE.Text = report.Scale.ResolutionE.ToString();
                    mainwindow.cbUnit.Text = report.Scale.Unit.ToString();
                    mainwindow.cbScaleClass.Text = report.Scale.ScaleClass.ToString();

                    mainwindow.txtPlace.Text = report.Place;
                    mainwindow.cbTechnician.Text = report.Technician;
                    mainwindow.cbManager.Text = report.Manager;
                    mainwindow.cbStatusReport.Text = report.Status.ToString();

                    mainwindow.txtRepMassApply.Text = report.RepTest.RepMass.ToString();
                    mainwindow.txtRepRead1.Text = report.RepTest.RepRead1.ToString();
                    mainwindow.txtRepRead2.Text = report.RepTest.RepRead2.ToString();

                    mainwindow.txtMobReadBefore.Text = report.MobTest.MobBefore.ToString();
                    mainwindow.txtMobOverLoad.Text = report.MobTest.MobLoad.ToString();
                    mainwindow.txtMobReadAfter.Text = report.MobTest.MobAfter.ToString();

                    mainwindow.cbEccTestType.Text = report.EccTest.Type.ToString();
                    mainwindow.txtEccLoad.Text = report.EccTest.EccLoad.ToString();
                    mainwindow.txtEccReadA.Text = report.EccTest.A.ToString();
                    mainwindow.txtEccReadB.Text = report.EccTest.B.ToString();
                    mainwindow.txtEccReadC.Text = report.EccTest.C.ToString();
                    mainwindow.txtEccReadD.Text = report.EccTest.D.ToString();
                    mainwindow.txtEccReadE.Text = report.EccTest.E.ToString();
                    mainwindow.txtEccReadF.Text = report.EccTest.F.ToString();


                }
            }
        }

    }
}