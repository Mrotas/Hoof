using System.ComponentModel;

namespace Domain.AnnualPlan.Models
{
    public class FodderPlanModel
    {
        public int? Type { get; set; }
        public double? Ton { get; set; }
        public int MarketingYearId { get; set; }
    }
    public enum Fodder
    {
        [Description("Sucha")]
        Dry = 1,

        [Description("Soczysta")]
        Juicy = 2,

        [Description("Treściwa")]
        Pithy = 3,

        [Description("Sól")]
        Salt = 4
    }
}
