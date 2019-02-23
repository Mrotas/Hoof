using System.ComponentModel.DataAnnotations;

namespace Domain.EmploymentPlan.ViewModels
{
    public class EmploymentPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp.")]
        public int EmploymentType { get; set; }

        [Display(Name = "Zatrudnienie")]
        public string EmploymentTypeName { get; set; }

        [Display(Name = "Liczba")]
        public double Count { get; set; }
    }
}
