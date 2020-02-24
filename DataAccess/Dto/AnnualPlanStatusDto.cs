using System;

namespace DataAccess.Dto
{
    public class AnnualPlanStatusDto
    {
        public int Id { get; set; }
        public int MarketingYearId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
