using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Loss
    {
        [Key]
        public int Id { get; set; }
        public int LossGameId { get; set; }
        public int RegionId { get; set; }
        public bool SanitaryLoss { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
