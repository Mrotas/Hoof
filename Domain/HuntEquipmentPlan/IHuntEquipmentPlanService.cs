using Domain.HuntEquipmentPlan.ViewModels;

namespace Domain.HuntEquipmentPlan
{
    public interface IHuntEquipmentPlanService
    {
        HuntEquipmentPlanBaseViewModel GetHuntEquipmentPlanViewModel(int marketingYearId);
        void AddHuntEquipmentPlan(HuntEquipmentPlanViewModel model, int marketingYearId);
    }
}