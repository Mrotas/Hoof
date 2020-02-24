using System.Collections.Generic;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.DeerLicker.ViewModels
{
    public class DeerLickerBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public IList<DeerLickerViewModel> DeerLickerViewModels { get; set; }
    }
}
