using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Catch
    {
        [Key]
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int GameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
