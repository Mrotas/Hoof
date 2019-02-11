using System.ComponentModel.DataAnnotations;

namespace Domain.GameHuntPlan.ViewModels
{
    public class GameHuntPlanViewModel
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

        [Display(Name = "Odstrzał")]
        public int? Cull { get; set; }

        [Display(Name = "Odłów")]
        public int? Catch { get; set; }
    }
}
