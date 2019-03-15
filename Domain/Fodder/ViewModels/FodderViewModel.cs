using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Fodder.ViewModels
{
    public class FodderViewModel
    {
        [Display(Name = "Lp.")]
        public int Id { get; set; }

        public int Type { get; set; }

        [Display(Name = "Nazwa")]
        public string TypeName { get; set; }

        [Display(Name = "Kilogramy")]
        public int Kilograms { get; set; }
        
        [Display(Name = "Gospodarz")]
        public string Owner { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }
    }
}
