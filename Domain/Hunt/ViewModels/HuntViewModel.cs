﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Hunt.ViewModels
{
    public class HuntViewModel
    {
        [Display(Name = "Imię")]
        public string HuntsmanName { get; set; }

        [Display(Name = "Nazwisko")]
        public string HuntsmanLastName { get; set; }

        [Display(Name = "Myśliwy")]
        public string Huntsman => $"{HuntsmanName} {HuntsmanLastName}";

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        public string DateString => Date.ToString("dd/MM/yyyy");

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Obwód")]
        public int Circuit { get; set; }

        [Display(Name = "Rewir")]
        public int District { get; set; }

        public int HuntedGameId { get; set; }
        
        [Display(Name = "Zwierzyna")]
        public string GameKindName { get; set; }

        [Display(Name = "Rodzaj zwierzyny")]
        public string GameSubKindName { get; set; }

        [Display(Name = "Klasa zwierzyny")]
        public string GameClass { get; set; }

        [Display(Name = "Waga zwierzyny")]
        public string GameWeight { get; set; }

        [Display(Name = "Strzały")]
        public int? Shots { get; set; }
    }
}
