using OfficeOpenXml;
using ReportApp.Data;
using ReportApp.Models;
using System.IO;

namespace ReportApp.Services;

public class ExcelImporter
{
    public void ImportExcelToDb(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(fileInfo))
        using (var context = new AppDbContext())
        {
            var worksheet = package.Workbook.Worksheets[0]; // Lê a primeira aba do Excel
            var worksheetWeight = package.Workbook.Worksheets[1];
            int totalRows = worksheet.Dimension.Rows;

            for (int row = 2; row <= totalRows; row++) // Inicia na linha 2 para ignorar o cabeçalho
            {
                var name = worksheet.Cells[row, 1].Value?.ToString();
                var address = worksheet.Cells[row, 2].Value?.ToString();
                var city = worksheet.Cells[row, 3].Value?.ToString();
                var state = worksheet.Cells[row, 4].Value?.ToString();

                if (!string.IsNullOrEmpty(name))
                {
                   
                    if (!context.Customers.Any(c => c.Name == name))
                    {
                        var customer = new Customer
                        {
                            Name = name,
                            Address = address,
                            City = city,
                            State = state
                        };

                        context.Customers.Add(customer);
                    }
                }
            }
            for (int row = 2; row <= totalRows; row++) // Inicia na linha 2 para ignorar o cabeçalho
            {
                var tag = worksheetWeight.Cells[row, 1].Value?.ToString();
                var valueN = worksheetWeight.Cells[row, 2].Value?.ToString();


                if (!string.IsNullOrEmpty(tag))
                {

                    if (!context.Weights.Any(c => c.TagName == tag))
                    {
                        var weight = new Weight
                        {
                            TagName = tag,
                            NominalValue = double.Parse(valueN)
                        };

                        context.Weights.Add(weight);
                    }
                }
            }


            context.SaveChanges();
        }
    }
}
