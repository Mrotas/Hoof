﻿namespace DataAccess.Entities.AnnualPlan
{
    public class FodderPlan
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public double? Count { get; set; }
        public int Unit { get; set; }
        public int Year { get; set; }
    }
}
