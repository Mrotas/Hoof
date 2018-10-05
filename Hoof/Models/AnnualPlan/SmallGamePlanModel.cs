using System;

namespace Hoof.Models.AnnualPlan
{
    public class SmallGamePlanModel
    {
        public int Type { get; set; }
        public int SubType { get; set; }
        public int Class { get; set; }
        public int Shoot { get; set; }
        public int Trap { get; set; }
        public int Loss { get; set; }
        public DateTime Year { get; set; }
    }
}