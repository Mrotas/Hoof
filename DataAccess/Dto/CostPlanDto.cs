namespace DataAccess.Dto
{
    public class CostPlanDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public double Cost { get; set; }
        public int MarketingYearId { get; set; }
    }
}
