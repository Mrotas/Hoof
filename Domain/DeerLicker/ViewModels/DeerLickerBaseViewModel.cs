using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.DeerLicker.ViewModels
{
    public class DeerLickerBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<DeerLickerViewModel> DeerLickerViewModels { get; set; }
    }
}
