using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using ReportApp.Models;
using ReportApp.Models.Extensions;
using System.IO;
using iText.IO.Image;
using System.Text;
using Microsoft.Win32;



namespace ReportApp.Services;

public class PdfCreatorService
{
    private string destFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Pdf"));
    private string imagesPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Images"));
    private string weights = string.Empty;

    public PdfCreatorService()
    {                    
           Directory.CreateDirectory(destFolder);
    }

    public void Create(Calibration cal)
    {
        var saveFileDialog = new SaveFileDialog();

        saveFileDialog.Filter = "Arquivo PDF (*.pdf)|*.pdf|Todos os arquivos (*.*)|*.*";
        saveFileDialog.Title = "Salvar Arquivo PDF";
        saveFileDialog.DefaultExt = ".pdf";
        saveFileDialog.AddExtension = true;
        saveFileDialog.FileName = $"{cal.ReportId}-{cal.Customer.Name}";

        if (saveFileDialog.ShowDialog() == true)
        {
            destFolder = saveFileDialog.FileName;
        }
        else
        {
            return;
        }

        
        FileInfo file = new FileInfo(destFolder);
        file.Directory?.Create();

        PdfWriter writer = new PdfWriter(destFolder);
        PdfDocument pdf = new PdfDocument(writer);
        Document doc = new Document(pdf, iText.Kernel.Geom.PageSize.A4);

        Image logoDigiRbc = new Image(ImageDataFactory.Create(Path.Combine(imagesPath, "logo-digi-rbc.JPG")));
        logoDigiRbc.ScaleToFit(300, 550);
        Image imgEccTypeA = new Image(ImageDataFactory.Create(Path.Combine(imagesPath, "EccTestTypeA.JPG")));
        imgEccTypeA.ScaleToFit(100, 100);
        Image imgEccTypeB = new Image(ImageDataFactory.Create(Path.Combine(imagesPath, "EccTestTypeB.JPG")));
        imgEccTypeB.ScaleToFit(100, 100);

        var t1 = new Table(6);
        t1.SetWidth(520);
        t1.SetFontSize(8).SetTextAlignment(TextAlignment.CENTER).SetBold();

        t1.AddCell(new Cell(3, 3).Add(logoDigiRbc.SetHorizontalAlignment(HorizontalAlignment.CENTER)));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph($"CERTIFICADO")).SetBackgroundColor(ColorConstants.DARK_GRAY).SetFontColor(ColorConstants.WHITE).SetFontSize(12));
        t1.AddCell(new Cell(2, 3).Add(new Paragraph($"{cal.ReportId}").SetFontSize(16).SetVerticalAlignment(VerticalAlignment.MIDDLE)));

        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Cliente:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 5).Add(new Paragraph($"{cal.Customer.Name}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Endereço:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 5).Add(new Paragraph($"{cal.Customer.Address}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Cidade:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph($"{cal.Customer.City}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Estado:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Customer.State}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Fabricante:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 5).Add(new Paragraph($"{cal.Scale.Model}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Nº de série:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 5).Add(new Paragraph($"{cal.Scale.SerialNo}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("TAG:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph($"{cal.Scale.TagName}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Classe:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.ScaleClass.ScaleClassToString()}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Capacidade:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph($"{cal.Scale.Capacity}").SetTextAlignment(TextAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("UNIDADE:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.Unit.ToString().ToLower()}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"Resolução:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"d=")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.ResolutionD}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"e=")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Scale.ResolutionE}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("")));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 6).Add(new Paragraph("Ensaio de Mobilidade")).SetBackgroundColor(ColorConstants.DARK_GRAY).SetFontColor(ColorConstants.WHITE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("Leitura sem sobrecarga:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("Sobrecarga aplicada")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("Leitura após a sobrecarga:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.MobTest.MobBefore}")));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.MobTest.MobLoad}")));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.MobTest.MobAfter}")));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 3).Add(new Paragraph("Ensaio de Repetibilidade")).SetBackgroundColor(ColorConstants.DARK_GRAY).SetFontColor(ColorConstants.WHITE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Peso aplicado:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.RepTest.RepMass}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Leitura 1:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.RepTest.RepRead1}").SetHorizontalAlignment(HorizontalAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Leitura 2:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.RepTest.RepRead2}").SetHorizontalAlignment(HorizontalAlignment.LEFT)));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Leitura 3:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.RepTest.RepRead3}").SetHorizontalAlignment(HorizontalAlignment.LEFT)));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 6).Add(new Paragraph("Ensaio de Excentricidade")).SetBackgroundColor(ColorConstants.DARK_GRAY).SetFontColor(ColorConstants.WHITE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("TIPO DE ENSAIO:")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.Type}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Peso aplicado")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.EccLoad}{cal.Scale.Unit.UnitToString()}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("A")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.A}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        if (cal.EccTest.Type == "Industrial")
        {
            t1.AddCell(new Cell(6, 3)
            .Add(imgEccTypeA.SetHorizontalAlignment(HorizontalAlignment.CENTER))
            .SetVerticalAlignment(VerticalAlignment.MIDDLE));

        }
        else if (cal.EccTest.Type == "Rodoviária")
        {
            t1.AddCell(new Cell(6, 3).Add(imgEccTypeB.SetHorizontalAlignment(HorizontalAlignment.CENTER)).SetVerticalAlignment(VerticalAlignment.MIDDLE));

        }
        else
        {
            t1.AddCell(new Cell(6, 3).Add(new Paragraph("Não se aplica")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER));

        }

        t1.AddCell(new Cell(1, 1).Add(new Paragraph("B")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.B}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("C")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.C}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("D")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.D}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("E")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.E}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("F")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.EccTest.F}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("")));

        t1.AddCell(new Cell(1, 6).Add(new Paragraph("Teste de Pesagem")).SetBackgroundColor(ColorConstants.DARK_GRAY).SetFontColor(ColorConstants.WHITE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("CARGA")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("LEITURA")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("ERRO")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 3).Add(new Paragraph("PADRÕES DE TRABALHO UTILIZADOS")).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[0].WLoad}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[0].WRead}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[0].WError}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
        StringBuilder sb = new();
        foreach (Weight w in cal.Weights)
        {
            sb.Append($"{w.TagName}  ");
            weights = sb.ToString();
        }

        t1.AddCell(new Cell(10, 3).Add(new Paragraph($"{weights}")).SetVerticalAlignment(VerticalAlignment.TOP).SetHorizontalAlignment(HorizontalAlignment.LEFT));
        for (int i = 1; i < 10; i++)
        {
            if (i < cal.WeightTest.Count)
            {
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[i].WLoad}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[i].WRead}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.WeightTest[i].WError}")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            }
            else
            {
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"--")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"--")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                t1.AddCell(new Cell(1, 1).Add(new Paragraph($"--")).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            }
        }
        t1.AddCell(new Cell(3, 6).Add(new Paragraph("- Incerteza expandida baseada em uma incerteza combinada multiplicada por um fator de abrangência k=2, para um nível de confiança de aproximadamente 95%.\r\n- INCERTEZA EXPANDIDA DAS LEITURAS EFETUADAS = 0,04%.").SetTextAlignment(TextAlignment.LEFT)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("EXECUTOR").SetFontSize(7)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.Technician}")));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("DATA DO ENSAIO:").SetFontSize(7).SetTextAlignment(TextAlignment.LEFT)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.DateCal.ToString("dd/MM/yyyy")}")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("RESPONSÁVEL:").SetFontSize(7)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.Manager}")));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("TOLERÂNCIA CONFORME:").SetFontSize(7).SetTextAlignment(TextAlignment.LEFT)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("Portaria 157/22")));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph("LOCAL:").SetFontSize(7)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph($"{cal.Place}")));
        t1.AddCell(new Cell(1, 2).Add(new Paragraph("SITUAÇÃO DO INSTRUMENTO:").SetFontSize(7).SetTextAlignment(TextAlignment.LEFT)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
        t1.AddCell(new Cell(1, 1).Add(new Paragraph($"{cal.Status}")));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("Procedimento de Referência: PR17-DT-CCD_certificado de Conformidade DIGI-TRON").SetFontSize(6)));
        t1.AddCell(new Cell(1, 6).Add(new Paragraph("Fábrica, Administração, Vendas, Locação e Assistência Técnica. Fone: 41-3377-1577 | E-mail: conformidade@digitronbalancas.com.br").SetFontSize(6)));



        doc.Add(t1);

        doc.Close();

        System.Windows.MessageBox.Show($"Pdf criado com sucesso em:\n {destFolder}", "Pdf Criado", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

    }
}
