using System;

namespace DataAccess.Entities
{
    public class GameLoss
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public bool SanitaryLoss { get; set; }
        public DateTime Date { get; set; }
    }
}
