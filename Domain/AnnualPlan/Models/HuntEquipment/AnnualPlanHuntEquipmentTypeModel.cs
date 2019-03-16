using Common.Enums;

namespace Domain.AnnualPlan.Models.HuntEquipment
{
    public class AnnualPlanHuntEquipmentTypeModel
    {
        public HuntEquipmentType HuntEquipmentType { get; set; }
        public string HuntEquipmentTypeName { get; set; }
        public int PreviousPlan { get; set; }
        public int Execution { get; set; }
        public int CurrentState { get; set; }
        public int FutureState { get; set; }
    }
}
