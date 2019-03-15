using Common.Enums;

namespace Domain.AnnualPlan.Models.Fodder
{
    public class AnnualPlanFodderTypeModel
    {
        public FodderType FodderType { get; set; }
        public string FodderTypeName { get; set; }
        public double Plan { get; set; }
        public double Execution { get; set; }
    }
}
