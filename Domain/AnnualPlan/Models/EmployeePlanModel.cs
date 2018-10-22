namespace Domain.AnnualPlan.Models
{
    public class EmployeePlanModel
    {
        public int? Type { get; set; }
        public int? Count { get; set; }
        public int Year { get; set; }
    }
    public enum EmploymentType
    {
        FullTime = 60,
        PartTime = 61
    }
}
