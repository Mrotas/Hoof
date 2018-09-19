using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Hunt")]
    public class Hunt
    {
        public int Id { get; set; }
        public int HuntsmanId { get; set; }
        public int GameId { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
        public int Shots { get; set; }
    }
}
