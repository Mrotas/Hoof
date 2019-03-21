using System;

namespace Domain.CarcassRevenue.ViewModels
{
    public class CarcassRevenueViewModel
    {
        public int Id { get; set; }
        public int HuntedGameId { get; set; }
        public int GameKind { get; set; }
        public string GameKindName { get; set; }
        public int? GameSubKind { get; set; }
        public string GameSubKindName { get; set; }
        public int? Class { get; set; }
        public string ClassName { get; set; }
        public double Revenue { get; set; }
        public double CarcassWeight { get; set; }
        public DateTime HuntDate { get; set; }
    }
}
