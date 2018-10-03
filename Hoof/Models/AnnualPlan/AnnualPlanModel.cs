namespace Hoof.Models.AnnualPlan
{
    public class AnnualPlanModel
    {
        public HuntEquipmentModel HuntEquipmentModel { get; set; }
        public FodderModel FodderModel { get; set; }
        public EconomyModel EconomyModel { get; set; }
        public CostModel CostModel { get; set; }
        public BigGameModel BigGameModel { get; set; }
        public SmallGameModel SmallGameModel { get; set; }
    }
}