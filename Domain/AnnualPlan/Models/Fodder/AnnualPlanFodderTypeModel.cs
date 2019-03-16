using Common.Enums;

namespace Domain.AnnualPlan.Models.Fodder
{
    public class AnnualPlanFodderTypeModel
    {
        public FodderType FodderType { get; set; }
        public string FodderTypeName { get; set; }
        public double PreviousPlan { get; set; }
        public double Execution { get; set; }
        public double CurrentState { get; set; }
        public double FutureState { get; set; }
    }
}
