using System;
using Common;

namespace Domain.Report.Models
{
    public class MonthlyReportGameBaseModel
    {
        public int Culls { get; set; }
        public int Catches { get; set; }
        public int Losses { get; set; }
        public int LossesWithCatches => Losses + Catches;
        public int ExecutionTotal => Culls + Catches + Losses;
        public int HuntPlanCulls { get; set; }
        public int ExecutionLeft => HuntPlanCulls - ExecutionTotal;
        public string Note
        {
            get
            {
                if (HuntPlanCulls > 0)
                {
                    if (ExecutionLeft == 0)
                    {
                        return Text.GameAnnualPlanAccomplished;
                    }

                    if (ExecutionLeft < 0)
                    {
                        return Text.GameAnnualPlanExceeded;
                    }
                }

                return String.Empty;
            }
        }
    }
}
