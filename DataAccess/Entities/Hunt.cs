using System;

namespace DataAccess.Entities
{
    public class Hunt
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int? GameId { get; set; }
        public int? GameClass { get; set; }
        public double? GameWeight { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int? Shots { get; set; }
    }
}
