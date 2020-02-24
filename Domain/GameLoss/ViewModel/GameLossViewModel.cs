using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.GameLoss.ViewModel
{
    public class GameLossViewModel
    {
        [Display(Name = "Zwierzyna Gruba/Drobna")]
        public string TypeName { get; set; }

        [Display(Name = "Typ")]
        public string KindName { get; set; }

        [Display(Name = "Rodzaj")]
        public string SubKindName { get; set; }

        [Display(Name = "Klasa")]
        public string ClassName { get; set; }

        [Display(Name = "Postrzał sanitarny")]
        public bool SanitaryLoss { get; set; }

        [Display(Name = "Obwód")]
        public string Circuit { get; set; }

        [Display(Name = "Rewir")]
        public int? District { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Data zgłoszenia")]
        public DateTime Date { get; set; }

        public string DateString => Date.ToString("dd/MM/yyyy");
    }
}
