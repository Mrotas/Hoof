using System.Collections.Generic;
using Domain.AnnualPlan.GamePlan.Models;
using Domain.AnnualPlan.Models;

namespace Domain.AnnualPlan.ViewModels
{
    public class AnnualPlanViewModel
    {
        public AnnualPlanModel CurrentAnnualPlanModel { get; set; }
        public AnnualPlanModel LastYearAnnualPlanModel { get; set; }
        public IList<GamePlanModel> GamePlanModel { get; set; }
    }
}
