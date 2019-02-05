using Domain.AnnualPlan.Models;
using Domain.AnnualPlan.Models.GamePlan;
using Domain.MarketingYear.Models;

namespace Domain.AnnualPlan.ViewModels
{
    public class AnnualPlanViewModel
    {
        public MarketingYearModel CurrentMarketingYearModel { get; set; }
        public MarketingYearModel PreviousMarketingYearModel { get; set; }
        public AnnualPlanModel CurrentAnnualPlanModel { get; set; }
        public AnnualPlanModel PreviousAnnualPlanModel { get; set; }
        public AnnualPlanGameModel BigGamePlanModel { get; set; }
        public AnnualPlanGameModel SmallGamePlanModel { get; set; }
    }
}
