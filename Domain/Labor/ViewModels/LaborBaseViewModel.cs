using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.Labor.ViewModels
{
    public class LaborBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<LaborViewModel> LaborViewModels { get; set; }
    }
}
