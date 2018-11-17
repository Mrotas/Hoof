using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Hunt.ViewModels
{
    public class HuntViewModel
    {
        [Display(Name = "Imię")]
        public string HuntsmanName { get; set; }

        [Display(Name = "Nazwisko")]
        public string HuntsmanLastName { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Obwód")]
        public int Circuit { get; set; }

        [Display(Name = "Rewir")]
        public int District { get; set; }

        [Display(Name = "Zwierzyna Gruba/Drobna")]
        public string GameType { get; set; }

        [Display(Name = "Nazwa zwierzyny")]
        public string GameSubType { get; set; }

        [Display(Name = "Klasa zwierzyny")]
        public string GameClass { get; set; }

        [Display(Name = "Strzały")]
        public int? Shots { get; set; }
    }
}
