using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class LossGame
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? Class { get; set; }
    }
}
