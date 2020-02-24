using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.FodderPlan.ViewModels
{
    public class FodderPlanBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearModel.Start:dd/MM/yyyy} - {MarketingYearModel.End:dd/MM/yyyy}";

        public IList<FodderPlanViewModel> FodderPlanViewModels { get; set; }
    }
}
