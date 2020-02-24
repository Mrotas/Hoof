using System.ComponentModel.DataAnnotations;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;
using Domain.TrunkFoodPlan.Models;

namespace Domain.TrunkFoodPlan.ViewModels
{
    public class TrunkFoodPlanViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearModel.Start:dd/MM/yyyy} - {MarketingYearModel.End:dd/MM/yyyy}";
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }
    }
}
