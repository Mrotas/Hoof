using System;

namespace Domain.GameLoss.Model
{
    public class GameLossModel
    {
        public int GameType { get; set; }
        public int GameKind { get; set; }
        public int? GameSubKind { get; set; }
        public bool SanitaryLoss { get; set; }
        public string City { get; set; }
        public int? Circuit { get; set; }
        public int? District { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
