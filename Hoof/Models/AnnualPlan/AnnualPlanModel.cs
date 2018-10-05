using Hoof.Models.AnnualPlan.Economy;

namespace Hoof.Models.AnnualPlan
{
    public class AnnualPlanModel
    {
        public EconomyPlanModel EconomyPlanModel { get; set; }
        public HuntEquipmentPlanModel HuntEquipmentPlanModel { get; set; }
        public FodderPlanModel FodderPlanModel { get; set; }
        public CostPlanModel CostModel { get; set; }
        public BigGamePlanModel BigGameModel { get; set; }
        public SmallGamePlanModel SmallGameModel { get; set; }
    }
}