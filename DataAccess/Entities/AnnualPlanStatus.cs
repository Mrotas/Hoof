using System;

namespace DataAccess.Entities
{
    public class AnnualPlanStatus
    {
        public int Id { get; set; }
        public int MarketingYearId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
