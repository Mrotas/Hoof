using System.ComponentModel.DataAnnotations;
using Domain.AnnualPlanStatus.Models;
using Domain.FieldPlan.Models;
using Domain.MarketingYear.Models;

namespace Domain.FieldPlan.ViewModels
{
    public class FieldPlanViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearModel.Start:dd/MM/yyyy} - {MarketingYearModel.End:dd/MM/yyyy}";

        public FiledPlanModel FiledPlanModel { get; set; }
    }
}
