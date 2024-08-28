using ReportApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Models
{
    public class Calibration
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
        public RepTest RepTest { get; set; }
        public MobTest MobTest { get; set; }
        public EccTest EccTest { get; set; }
        public List<WeightTest> WeightTest { get; set; }
        public List<Weight> Weights { get; set; }

        public Calibration()
        {
            
        }
    }

    
}
