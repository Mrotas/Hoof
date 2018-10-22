using Domain.AnnualPlan.Models;

namespace Domain.AnnualPlan.ViewModels
{
    public class AnnualPlanViewModel
    {
        public AnnualPlanModel CurrentAnnualPlanModel { get; set; }
        public AnnualPlanModel LastYearAnnualPlanModel { get; set; }
    }
}
