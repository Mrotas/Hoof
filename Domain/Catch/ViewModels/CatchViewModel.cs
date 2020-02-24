using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Catch.ViewModels
{
    public class CatchViewModel
    {
        [Display(Name = "Imię")]
        public string HuntsmanName { get; set; }

        [Display(Name = "Nazwisko")]
        public string HuntsmanLastName { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        public string DateString => Date.ToString("dd/MM/yyyy");

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Obwód")]
        public int Circuit { get; set; }

        [Display(Name = "Rewir")]
        public int District { get; set; }

        [Display(Name = "Zwierzyna")]
        public string GameKindName { get; set; }

        [Display(Name = "Rodzaj zwierzyny")]
        public string GameSubKindName { get; set; }

        [Display(Name = "Złowiono")]
        public int? Count { get; set; }
    }
}
