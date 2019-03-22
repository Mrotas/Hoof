using System;

namespace Domain.Labor.ViewModels
{
    public class LaborViewModel
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public string HuntsmanName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
