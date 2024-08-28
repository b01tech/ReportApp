﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReportApp.Models
{
    public class RepTest
    {
        public double RepMass { get; set; }
        public double RepRead1 { get; set; }
        public double RepRead2 { get; set; }
    }

    public class MobTest
    {
        public double MobBefore { get; set; }
        public double MobLoad { get; set; }
        public double MobAfter { get; set; }
    }

    public class EccTest
    {
        public string Type { get; set; }
        public double? EccLoad { get; set; }
        public double? A { get; set; }
        public double? B { get; set; }
        public double? C { get; set; }
        public double? D { get; set; }
        public double? E { get; set; }
        public double? F { get; set; }
    }

    public class WeightTest
    {
        [Key]
        public int Id { get; set; }  // Chave primária que não é gerada automaticamente

        public int CalibrationId { get; set; } // Chave estrangeira para associar a Calibration
        public double WLoad { get; set; }
        public double WRead { get; set; }

        // Propriedade de navegação
        [ForeignKey("CalibrationId")]
        public Calibration Calibration { get; set; }
    }
}
