﻿using System;

namespace DataAccess.Entities
{
    public class GameLoss
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public bool SanitaryLoss { get; set; }
        public string City { get; set; }
        public int? Circuit { get; set; }
        public int? District { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
