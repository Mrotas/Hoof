using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.Pasture.ViewModels
{
    public class PastureBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<PastureViewModel> PastureViewModels { get; set; }  
    }
}
