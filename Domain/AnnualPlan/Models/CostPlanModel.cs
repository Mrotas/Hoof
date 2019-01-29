namespace Domain.AnnualPlan.Models
{
    public class CostPlanModel
    {
        public double? Cost { get; set; }
        public int? Type { get; set; }
        public int MarketingYearId { get; set; }
    }

    public enum CostType
    {
        Cost = 1,
        Revenue = 2
    }
}
