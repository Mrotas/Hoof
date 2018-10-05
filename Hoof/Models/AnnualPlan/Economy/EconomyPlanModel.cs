using System.Collections.Generic;

namespace Hoof.Models.AnnualPlan.Economy
{
    public class EconomyPlanModel
    {
        public IList<EmployeePlanModel> EmployeePlanModels { get; set; }
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }
        public BarrierPlanModel BarrierPlanModel { get; set; }
        public FieldPlanModel FieldPlanModeldel { get; set; }
        public DamagedFieldPlanModel DamagedFieldPlanModel { get; set; }
    }
}