﻿namespace Domain.AnnualPlan.Models
{
    public class FieldPlanModel
    {
        public double PreviousPlan { get; set; }
        public double Execution { get; set; }
        public double CurrentState { get; set; }
        public double FutureState { get; set; }
    }
}
