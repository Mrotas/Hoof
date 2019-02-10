using System;

namespace DataAccess.Dto
{
    public class CatchDto
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int GameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
