namespace Domain.AnnualPlan.Models
{
    public class BarrierPlanModel
    {
        public double PreviousLengthPlan { get; set; }
        public double LengthExecution { get; set; }
        public double CurrentStateLength { get; set; }
        public double FutureStateLength { get; set; }
        public int PreviousCountPlan { get; set; }
        public int CountExecution { get; set; }
        public int CurrentStateCount { get; set; }
        public int FutureStateCount { get; set; }
    }
}
