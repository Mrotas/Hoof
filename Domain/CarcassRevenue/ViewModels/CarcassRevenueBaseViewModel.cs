using System.Collections.Generic;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.CarcassRevenue.ViewModels
{
    public class CarcassRevenueBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public IList<CarcassRevenueViewModel> CarcassRevenueViewModels { get; set; }
    }
}
