using System.ComponentModel.DataAnnotations;

namespace Domain.GameHuntPlan.ViewModels
{
    public class GameHuntPlanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lp.")]
        public int GameId { get; set; }

        public int GameType { get; set; }

        public int GameKind { get; set; }

        [Display(Name = "Zwierzyna")]
        public string GameKindName { get; set; }

        public int? GameSubKind { get; set; }

        [Display(Name = "Rodzaj")]
        public string GameSubKindName { get; set; }

        public int? Class { get; set; }

        [Display(Name = "Klasa")]
        public string ClassName { get; set; }

        [Display(Name = "Odstrzał")]
        public int? Cull { get; set; }

        [Display(Name = "Odłów")]
        public int? Catch { get; set; }
    }
}
