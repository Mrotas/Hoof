﻿namespace DataAccess.Entities
{
    public class GameSettlementCountPlan
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int? Count { get; set; }
        public int Year { get; set; }
    }
}