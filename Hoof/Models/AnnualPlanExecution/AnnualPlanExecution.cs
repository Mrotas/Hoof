namespace Hoof.Models.AnnualPlanExecution
{
    public class AnnualPlanExecution
    {
        public EconomyExecutionModel EconomyExecutionModel { get; set; }
        public HuntEquipmentExecutionModel HuntEquipmentExecutionModel { get; set; }
        public FodderExecutionModel FodderExecutionModel { get; set; }
        public CostExecutionModel CostExecutionModel { get; set; }
        public BigGameExecutionModel BigGameExecutionModel { get; set; }
        public SmallGameExecutionModel SmallGameExecutionModel { get; set; }
    }
}