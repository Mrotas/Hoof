namespace Domain.AnnualPlan.Models
{
    public class EmploymentPlanModel
    {
        public int? EmploymentType { get; set; }
        public int? Count { get; set; }
        public int MarketingYearId { get; set; }
    }
    public enum EmploymentType
    {
        FullTime = 1,
        PartTime = 2
    }
}