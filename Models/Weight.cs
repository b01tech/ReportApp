using System.ComponentModel.DataAnnotations;

namespace ReportApp.Models;

public class Weight
{
    [Key]
    public int WeightId { get; set; }
    public string TagName { get; set; }
    public double NominalValue { get; set; }
}
