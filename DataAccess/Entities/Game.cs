using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    [Table("Game")]
    public class Game
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public int Gender { get; set; }
        public double Weight { get; set; }
    }
}
