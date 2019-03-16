using Common.Enums;

namespace Domain.AnnualPlan.Models.Cost
{
    public class AnnualPlanCostTypeModel
    {
        public CostType CostType { get; set; }
        public string CostTypeName { get; set; }
        public double PreviousPlan { get; set; }
        public double Execution { get; set; }
        public double CurrentState { get; set; }
        public double FutureState { get; set; }
    }
}
