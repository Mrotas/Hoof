using System;

namespace DataAccess.Entities
{
    public class Hunt
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int? HuntedGameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int Shots { get; set; }
    }
}
