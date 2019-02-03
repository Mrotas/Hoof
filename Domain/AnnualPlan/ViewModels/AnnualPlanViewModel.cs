using System.Collections.Generic;
using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.Models.GamePlan;

namespace Domain.AnnualPlan.ViewModels
{
    public class AnnualPlanViewModel
    {
        public AnnualPlanModel CurrentAnnualPlanModel { get; set; }
        public AnnualPlanModel PreviousAnnualPlanModel { get; set; }
        public AnnualPlanGameModel BigGamePlanModel { get; set; }
        public AnnualPlanGameModel SmallGamePlanModel { get; set; }
    }
}
