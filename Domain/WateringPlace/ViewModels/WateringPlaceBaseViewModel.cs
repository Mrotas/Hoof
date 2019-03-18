using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.WateringPlace.ViewModels
{
    public class WateringPlaceBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public IList<WateringPlaceViewModel> WateringPlaceViewModels { get; set; }
    }
}
