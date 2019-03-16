using Domain.AnnualPlan.Models.Cost;
using Domain.AnnualPlan.Models.Employment;
using Domain.AnnualPlan.Models.Fodder;
using Domain.AnnualPlan.Models.HuntEquipment;

namespace Domain.AnnualPlan.Models
{
    public class AnnualPlanModel
    {
        public AnnualPlanEmploymentModel EmployeePlanModel { get; set; }
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }
        public BarrierPlanModel BarrierPlanModel { get; set; }
        public FieldPlanModel FieldPlanModel { get; set; }
        public DamagedFieldPlanModel DamagedFieldPlanModel { get; set; }
        public AnnualPlanHuntEquipmentModel HuntEquipmentPlanModel { get; set; }
        public AnnualPlanFodderModel FodderPlanModel { get; set; }
        public AnnualPlanCostModel CostPlanModel { get; set; }
    }
}
