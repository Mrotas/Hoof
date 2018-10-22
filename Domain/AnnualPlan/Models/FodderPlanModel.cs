using System.ComponentModel;

namespace Domain.AnnualPlan.Models
{
    public class FodderPlanModel
    {
        public int? Type { get; set; }
        public double? Count { get; set; }
        public int Unit { get; set; }
        public int Year { get; set; }
    }
    public enum Fodder
    {
        [Description("Sucha")]
        Dry = 7,

        [Description("Soczysta")]
        Juicy = 8,

        [Description("Treściwa")]
        Pithy = 9,

        [Description("Sól")]
        Salt = 10
    }
}
