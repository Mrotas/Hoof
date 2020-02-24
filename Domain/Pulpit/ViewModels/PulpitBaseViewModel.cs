using System.Collections.Generic;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.Pulpit.ViewModels
{
    public class PulpitBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public IList<PulpitViewModel> PulpitViewModels { get; set; }
    }
}
