using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ReportApp.Models;
using ReportApp.Models.Extensions;
using System.IO;



namespace ReportApp.Services;

public class PdfCreatorService
{
    private string destFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Pdf"));
    private string imagesPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources\Images"));

    public PdfCreatorService()
    {
        Directory.CreateDirectory(destFolder);
    }

    public void Create(Calibration cal)
    {
        string pdfPath = Path.Combine(destFolder, $"{cal.ReportId}.pdf");
        FileInfo file = new FileInfo(pdfPath);
        file.Directory?.Create();

        PdfWriter writer = new PdfWriter(pdfPath);
        PdfDocument pdf = new PdfDocument(writer);
        Document doc = new Document(pdf, iText.Kernel.Geom.PageSize.A4);

        var t1 = new Table(32);
        t1.SetWidth(520);
        t1.SetFontSize(10).SetTextAlignment(TextAlignment.CENTER).SetBold();

        t1.AddCell(new Cell(1, 20).Add(new Paragraph("Especialista em Pesagem Industrial")).SetFontSize(10).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 32).Add(new Paragraph($"CERTIFICADO DE CALIBRAÇÃO    N°   {cal.ReportId}")).SetFontSize(15)).SetVerticalAlignment(VerticalAlignment.MIDDLE);
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Cliente:")));
        t1.AddCell(new Cell(1, 20).Add(new Paragraph($"{cal.Customer.Name}")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Endereço:")));
        t1.AddCell(new Cell(1, 20).Add(new Paragraph($"{cal.Customer.Address}")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Cidade:")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph($"{cal.Customer.City}")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("Estado:")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph($"{cal.Customer.State}")));
        t1.AddCell(new Cell(1, 32).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Fabricante:")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph($"{cal.Scale.Model}")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("Classe:")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph($"{cal.Scale.ScaleClass.ScaleClassToString()}")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Nº de série:")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph($"{cal.Scale.SerialNo}")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("TAG:")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph($"{cal.Scale.TagName}")));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Capacidade:")));
        t1.AddCell(new Cell(1, 10).Add(new Paragraph($"{cal.Scale.Capacity}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.Unit.ToString()}")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph($"Resolução:")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"d=")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.ResolutionD}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"e=")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.ResolutionE}")));
        t1.AddCell(new Cell(1, 32).Add(new Paragraph("")));


        t1.AddCell(new Cell(1, 16).Add(new Paragraph("Ensaio de Repetibilidade")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("")));
        t1.AddCell(new Cell(1, 16).Add(new Paragraph("Ensaio de Mobilidade")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("Peso aplicado:")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.RepTest.RepMass}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("Leitura sem sobrecarga:")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.MobTest.MobBefore}")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("1°:")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.RepTest.RepRead1}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("Sobrecarga aplicada")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.MobTest.MobLoad}")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("2°:")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.RepTest.RepRead2}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph("Leitura após a sobrecarga:")));
        t1.AddCell(new Cell(1, 8).Add(new Paragraph($"{cal.MobTest.MobAfter}")));
        t1.AddCell(new Cell(1, 32).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 24).Add(new Paragraph("Ensaio de Excentricidade")));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("Carga Aplicada")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph($"{cal.EccTest.EccLoad}{cal.Scale.Unit.UnitToString()}")));
        t1.AddCell(new Cell(4, 20).Add(new Paragraph("IMAGEM")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 12).Add(new Paragraph("Leituras")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetUnderline());
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("1")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("2")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("3")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("4")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("5")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("6")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 4).Add(new Paragraph("0000kg")).SetVerticalAlignment(VerticalAlignment.MIDDLE));


        doc.Add(t1);

        doc.Close();

        System.Windows.MessageBox.Show($"Pdf criado com sucesso em {pdfPath}", "Pdf Criado", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

    }
}
