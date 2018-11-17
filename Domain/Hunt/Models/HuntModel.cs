using System;

namespace Domain.Hunt.Models
{
    public class HuntModel
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int? GameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int? Shots { get; set; }
    }
}
