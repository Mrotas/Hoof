using System;
using System.ComponentModel.DataAnnotations;
using Domain.TrunkFoodPlan.Models;

namespace Domain.TrunkFoodPlan.ViewModels
{
    public class TrunkFoodPlanViewModel
    {
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }

        public int MarketingYearId { get; set; }
        public DateTime MarketingYearStart { get; set; }
        public DateTime MarketingYearEnd { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearStart:dd/MM/yyyy} - {MarketingYearEnd:dd/MM/yyyy}";
    }
}
