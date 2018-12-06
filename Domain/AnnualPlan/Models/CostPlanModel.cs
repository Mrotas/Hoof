﻿namespace Domain.AnnualPlan.Models
{
    public class CostPlanModel
    {
        public double? Cost { get; set; }
        public int? Type { get; set; }
        public int Year { get; set; }
    }

    public enum CostType
    {
        Cost = 62,
        Revenue = 63
    }
}