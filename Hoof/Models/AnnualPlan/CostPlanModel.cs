using System;

namespace Hoof.Models.AnnualPlan
{
    public class CostPlanModel
    {
        public double Cost { get; set; }
        public CostType Type { get; set; }
        public DateTime Year { get; set; }

        public enum CostType
        {
            Cost,
            Revenue
        }
    }
}