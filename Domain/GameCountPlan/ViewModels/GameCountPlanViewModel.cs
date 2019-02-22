using System.ComponentModel.DataAnnotations;

namespace Domain.GameCountPlan.ViewModels
{
    public class GameCountPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp")]
        public int GameId { get; set; }

        [Display(Name = "Zwierzyna")]
        public string GameKindName { get; set; }

        [Display(Name = "Rodzaj")]
        public string GameSubKindName { get; set; }

        [Display(Name = "Klasa")]
        public string GameClassName { get; set; }

        public int? Class { get; set; }

        [Display(Name = "Liczebność")]
        public int? Count { get; set; }
    }
}
