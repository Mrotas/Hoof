using System;

namespace Hoof.Models.AnnualPlan.Economy
{
    public class EmployeePlanModel
    {
        public int Count { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public DateTime Year { get; set; }
    }

    public enum EmploymentType
    {
        FullTime,
        PartTime
    }
}