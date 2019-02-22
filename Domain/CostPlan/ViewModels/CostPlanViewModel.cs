using System.ComponentModel.DataAnnotations;

namespace Domain.CostPlan.ViewModels
{
    public class CostPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp.")]
        public int Type { get; set; }

        [Display(Name = "Typ")]
        public string TypeName { get; set; }

        [Display(Name = "Koszt")]
        public double Cost { get; set; }
    }
}
