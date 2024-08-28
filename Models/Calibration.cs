using ReportApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Models
{
    internal class Calibration
    {
        [Key]
        public int CalibrationId { get; set; }

        public Customer Customer { get; set; }
        public Scale Scale { get; set; }

        public string ReportId { get; set; }
        public string Place { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCal { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReport { get; set; }
        public string Technician { get; set; }
        public string Manager { get; set; }
        public ReportStatus Status { get; set; }
        public double RepTest { get; set; }
        public double MobTest { get; set; }
        public double EccTest { get; set; }
        public double WeightTest { get; set; }
        public List<Weight> Weights { get; set; }
    }
}
