using Microsoft.EntityFrameworkCore;
using ReportApp.Data;
using ReportApp.Models;
using ReportApp.Models.Enums;
using ReportApp.Models.Extensions;
using ReportApp.Services;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ReportApp.Views;

public partial class MainWindow : Window
{
    private SortedSet<string> _users = User.GetUsers();
    private SortedSet<string> _managers = User.GetManagers();
    private List<string> _status = ReportStatusExtension.GetAllStatus();
    private List<string> _scaleClass = ScaleClassExtension.GetAllScaleClass();
    private List<string> _unit = UnitExtension.GetAllUnit();
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
    private static readonly Regex _regex = new Regex("[^0-9,]+");
    internal List<Weight> weightsList = new();
    private PdfCreatorService _pdfCreatorService = new();    


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

    public void AddWeight(List<Weight> weightList)
    {
        var weights = weightList.Select(w => w.TagName);
        spWeightsList.Children.Clear();
        foreach (var weight in weights)
        {
            var label = new Label
            {
                Content = weight
            };
            spWeightsList.Children.Add(label);
        }

        this.weightsList = weightList;
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
        ClearAllInputs(spCalInfo);
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
                    double error = readValue - loadValue;
                    weightError.Text = error.ToString($"F{CalcFloatPoint(txtResolutionD.Text)}");
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
                    EccLoad = double.TryParse(mainWindow.txtEccLoad?.Text, out var eccLoad) ? eccLoad : (double?)null,
                    A = double.TryParse(mainWindow.txtEccReadA?.Text, out var a) ? a : null,
                    B = double.TryParse(mainWindow.txtEccReadB?.Text, out var b) ? b : null,
                    C = double.TryParse(mainWindow.txtEccReadC?.Text, out var c) ? c : null,
                    D = double.TryParse(mainWindow.txtEccReadD?.Text, out var d) ? d : null,
                    E = double.TryParse(mainWindow.txtEccReadE?.Text, out var e) ? e : null,
                    F = double.TryParse(mainWindow.txtEccReadF?.Text, out var f) ? f : null,
                };

                var weightTestList = new List<WeightTest>();
                for (int i = 1; i <= 12; i++)
                {
                    var wLoad = mainWindow.FindName($"txtLoad{i}") as TextBox;
                    var wRead = mainWindow.FindName($"txtRead{i}") as TextBox;
                    if (!string.IsNullOrEmpty(wLoad.Text))
                    {
                        weightTestList.Add(
                            new WeightTest
                            {
                                WLoad = double.TryParse(wLoad.Text, out var wl) ? wl : 0,
                                WRead = double.TryParse(wRead.Text, out var wr) ? wr : 0
                            });

                    }

                }

                var weightsId = mainWindow.weightsList.Select(w => w.WeightId).ToList();

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
                    DateCal = DateTime.Parse(mainWindow.dateCal.Text),
                    DateReport = DateTime.Parse(mainWindow.dateReport.Text),
                    Weights = context.Weights.Where(w => weightsId.Contains(w.WeightId)).ToList(),
                    WeightTest = weightTestList,
                    Status = Enum.TryParse<ReportStatus>(mainWindow.cbStatusReport.Text, out var status) ? status : ReportStatus.Aprovado,
                    Scale = scale


                };
                if (context.Calibrations.Any(id => id.ReportId == mainWindow.txtReportId.Text))
                {
                    var result = MessageBox.Show($"O certificado {mainWindow.txtReportId.Text} já existe.\nDeseja substitui-lo?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        UpdateReport(cal, context);
                        MessageBox.Show($"Certificado {mainWindow.txtReportId.Text} salvo com sucesso.","Aviso",MessageBoxButton.OK);
                        _pdfCreatorService.Create(cal);
                    }
                    else
                    {
                        MessageBox.Show($"Certificado não foi salvo.","Aviso", MessageBoxButton.OK);
                        return;
                    }
                }
                else
                {
                    context.Calibrations.Add(cal);
                    context.SaveChanges();
                    MessageBox.Show($"Certificado {mainWindow.txtReportId.Text} salvo com sucesso.");
                    _pdfCreatorService.Create(cal);
                }


            }
            catch (Exception)
            {
                MessageBox.Show($"Erro ao salvar o relatório: Campos inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }



    private void btnSearchId_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        if (mainWindow != null)
        {
            LoadReport(mainWindow);
        }
    }

    private void AddWeighTests(List<WeightTest> weightTests, string res)
    {
        for (int i = 1; i <= weightTests.Count; i++)
        {
            var load = this.FindName($"txtLoad{i}") as TextBox;
            var read = this.FindName($"txtRead{i}") as TextBox;
            var error = this.FindName($"txtError{i}") as TextBox;
            var testLoad = weightTests[i - 1].WLoad.ToString();
            var testRead = weightTests[i - 1].WRead.ToString();
            var testError = weightTests[i - 1].WLoad - weightTests[i - 1].WRead;

            if (!string.IsNullOrEmpty(testLoad) && !string.IsNullOrEmpty(testRead))
            {
                load.Text = testLoad;
                read.Text = testRead;
                error.Text = testError.ToString($"F{CalcFloatPoint(res)}");

            }
        }

    }
    private void LoadReport(MainWindow mainwindow)
    {
        ClearAllInputs(spWeightTest);
        ClearAllInputs(spEccTest);
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
                    MessageBox.Show($"Certificado {mainwindow.txtReportId.Text} não localizado", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MessageBox.Show($"Informações do certificado {mainwindow.txtReportId.Text} carregadas", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    mainwindow.cbUnit.Text = report.Scale.Unit.UnitToString();
                    mainwindow.cbScaleClass.Text = report.Scale.ScaleClass.ScaleClassToString();

                    mainwindow.txtPlace.Text = report.Place;
                    mainwindow.cbTechnician.Text = report.Technician;
                    mainwindow.cbManager.Text = report.Manager;
                    mainwindow.cbStatusReport.Text = report.Status.ToString();
                    mainwindow.dateCal.Text = report.DateCal.ToString();
                    mainwindow.dateReport.Text = report.DateReport.ToString();

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

                    mainwindow.AddWeight(report.Weights);

                    mainwindow.AddWeighTests(report.WeightTest, mainwindow.txtResolutionD.Text);

                }
            }
        }

    }

    private void UpdateReport(Calibration cal, AppDbContext context)
    {

        var report = context.Calibrations
            .Include(c => c.Weights)
            .Include(c => c.Scale)
            .FirstOrDefault(c => c.ReportId == cal.ReportId);

        if (report != null)
        {

            report.Place = cal.Place;
            report.Technician = cal.Technician;
            report.Manager = cal.Manager;
            report.Customer = cal.Customer;
            report.RepTest = cal.RepTest;
            report.MobTest = cal.MobTest;
            report.EccTest = cal.EccTest;
            report.DateCal = cal.DateCal;
            report.DateReport = cal.DateReport;
            report.Status = cal.Status;
            report.Scale = cal.Scale;
            report.Weights.Clear();            
            report.Weights = cal.Weights;
            report.WeightTest = cal.WeightTest;



            context.Calibrations.Update(report);
            context.SaveChanges();
        }
    }

    private int CalcFloatPoint(string res)
    {
        res = res.Replace(",", ".");
        int floatPoint = res.Length - res.IndexOf(".") - 1;
        if (floatPoint == -1)
            return 0;

        return floatPoint;
    }
}