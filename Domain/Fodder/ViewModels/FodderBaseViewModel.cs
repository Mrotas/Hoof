using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.Fodder.ViewModels
{
    public class FodderBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<FodderViewModel> FodderViewModels { get; set; }
    }
}
