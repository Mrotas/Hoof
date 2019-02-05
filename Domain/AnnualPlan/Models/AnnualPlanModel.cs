namespace Domain.AnnualPlan.Models
{
    public class AnnualPlanModel
    {
        public EmploymentPlanModel EmployeePlanModel { get; set; }
        public TrunkFoodPlanModel TrunkFoodPlanModel { get; set; }
        public BarrierPlanModel BarrierPlanModel { get; set; }
        public FieldPlanModel FieldPlanModel { get; set; }
        public DamagedFieldPlanModel DamagedFieldPlanModel { get; set; }
        public HuntEquipmentPlanModel HuntEquipmentPlanModel { get; set; }
        public FodderPlanModel FodderPlanModel { get; set; }
        public CostPlanModel CostPlanModel { get; set; }
    }
}
