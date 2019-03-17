using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.Pulpit.ViewModels
{
    public class PulpitBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<PulpitViewModel> PulpitViewModels { get; set; }

    }
}
