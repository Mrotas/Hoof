using System;

namespace Hoof.Models.AnnualPlan
{
    public class FodderPlanModel
    {
        public int Type { get; set; }
        public double Count { get; set; }
        public string Unit { get; set; }
        public DateTime Year { get; set; }
    }
}