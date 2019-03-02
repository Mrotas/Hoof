using System;
using System.ComponentModel.DataAnnotations;
using Domain.FieldPlan.Models;

namespace Domain.FieldPlan.ViewModels
{
    public class FieldPlanViewModel
    {
        public FiledPlanModel FiledPlanModel { get; set; }

        public int MarketingYearId { get; set; }
        public DateTime MarketingYearStart { get; set; }
        public DateTime MarketingYearEnd { get; set; }

        [Display(Name = "Rok gospodarczy")]
        public string MarketingYear => $"{MarketingYearStart:dd/MM/yyyy} - {MarketingYearEnd:dd/MM/yyyy}";
    }
}
