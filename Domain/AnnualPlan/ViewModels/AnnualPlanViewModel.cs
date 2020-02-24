using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.Models.GamePlan;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.AnnualPlan.ViewModels
{
    public class AnnualPlanViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public AnnualPlanModel AnnualPlanModel { get; set; }
        public AnnualPlanGameModel BigGamePlanModel { get; set; }
        public AnnualPlanGameModel SmallGamePlanModel { get; set; }
    }
}
