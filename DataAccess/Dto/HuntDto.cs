using System;

namespace DataAccess.Dto
{
    public class HuntDto
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int? HuntedGameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int Shots { get; set; }
    }
}
