using System;

namespace Domain.Catch.Models
{
    public class CatchCreateModel
    {
        public DateTime Date { get; set; }
        public string City { get; set; }
        public int Circuit { get; set; }
        public int District { get; set; }
        public int GameType { get; set; }
        public int GameKind { get; set; }
        public int? GameSubKind { get; set; }
        public int Count { get; set; }
    }
}
