using System.ComponentModel.DataAnnotations;

namespace Domain.HuntEquipmentPlan.ViewModels
{
    public class HuntEquipmentPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp.")]
        public int Type { get; set; }

        [Display(Name = "Nazwa")]
        public string TypeName { get; set; }

        [Display(Name = "Sztuki")]
        public int Count { get; set; }
    }
}
