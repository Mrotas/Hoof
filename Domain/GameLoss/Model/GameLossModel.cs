using System;

namespace Domain.GameLoss.Model
{
    public class GameLossModel
    {
        public int Type { get; set; }
        public int Kind { get; set; }
        public int? SubKind { get; set; }
        public int? Class { get; set; }
        public bool SanitaryLoss { get; set; }
        public string City { get; set; }
        public int Circuit { get; set; }
        public int District { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
