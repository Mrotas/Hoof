using System.ComponentModel.DataAnnotations;

namespace Domain.GameCountPlan.ViewModels
{
    public class GameCountPlanViewModel
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

        [Display(Name = "Liczebność")]
        public int Count { get; set; }
    }
}
