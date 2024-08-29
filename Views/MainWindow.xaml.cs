using System.Windows;
using System.Windows.Controls;

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
        if (cbEccTestType.SelectedValue.ToString() == "Não se aplica")
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
}
