using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Hunt.ViewModels
{
    public class HuntCreateViewModel
    {
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Obwód")]
        public int Circuit { get; set; }

        [Display(Name = "Rewir")]
        public int District { get; set; }

        [Display(Name = "Zwierzyna Gruba/Drobna")]
        public int? GameType { get; set; }

        [Display(Name = "Zwierzyna")]
        public int? GameSubType { get; set; }

        [Display(Name = "Klasa zwierzyny")]
        public int? GameClass { get; set; }

        [Display(Name = "Waga zwierzyny")]
        public double? GameWeight { get; set; }

        [Display(Name = "Strzały")]
        public int? Shots { get; set; }
    }
}
