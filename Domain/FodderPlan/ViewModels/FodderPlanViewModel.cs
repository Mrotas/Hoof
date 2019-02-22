using System.ComponentModel.DataAnnotations;

namespace Domain.FodderPlan.ViewModels
{
    public class FodderPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp.")]
        public int Type { get; set; }

        [Display(Name = "Nazwa")]
        public string TypeName { get; set; }

        [Display(Name = "Tony")]
        public double Ton { get; set; }
    }
}
