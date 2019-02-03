using System;

namespace DataAccess.Dto
{
    public class LossDto
    {
        public int Id { get; set; }
        public int LossGameId { get; set; }
        public int RegionId { get; set; }
        public bool SanitaryLoss { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
