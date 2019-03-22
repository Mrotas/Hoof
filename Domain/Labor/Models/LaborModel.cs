using System;

namespace Domain.Labor.Models
{
    public class LaborModel
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
