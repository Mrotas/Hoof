using Common.Enums;

namespace Domain.AnnualPlan.Models.Employment
{
    public class AnnualPlanEmploymentTypeModel
    {
        public EmploymentType EmploymentType { get; set; }
        public string EmploymentTypeName { get; set; }
        public int PreviousPlan { get; set; }
        public int Execution { get; set; }
        public int CurrentState { get; set; }
        public int FutureState { get; set; }
    }
}
