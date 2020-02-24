using System.Collections.Generic;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.WateringPlace.ViewModels
{
    public class WateringPlaceBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public IList<WateringPlaceViewModel> WateringPlaceViewModels { get; set; }
    }
}
