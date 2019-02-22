using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.HuntEquipmentPlan.ViewModels
{
    public class HuntEquipmentPlanBaseViewModel
    {
        public int MarketingYearId { get; set; }
        public DateTime MarketingYearStart { get; set; }
        public DateTime MarketingYearEnd { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearStart:dd/MM/yyyy} - {MarketingYearEnd:dd/MM/yyyy}";

        public IList<HuntEquipmentPlanViewModel> HuntEquipmentPlanViewModels { get; set; }
    }
}
