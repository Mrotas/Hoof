using System.Collections.Generic;

namespace Domain.AnnualPlan.Models
{
    public class AnnualPlanModel
    {
        public List<EmployeePlanModel> EmployeePlanModels { get; set; }
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }
        public BarrierPlanModel BarrierPlanModel { get; set; }
        public FieldPlanModel FieldPlanModel { get; set; }
        public DamagedFieldPlanModel DamagedFieldPlanModel { get; set; }
        public List<HuntEquipmentPlanModel> HuntEquipmentPlanModels { get; set; }
        public List<FodderPlanModel> FodderPlanModels { get; set; }
        public List<CostPlanModel> CostPlanModels { get; set; }
        public List<GamePlanModel> GamePlanModels { get; set; }
    }
}
