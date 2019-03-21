using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.CarcassRevenue.ViewModels
{
    public class CarcassRevenueBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<CarcassRevenueViewModel> CarcassRevenueViewModels { get; set; }
    }
}
