using Domain.HuntEquipmentPlan.ViewModels;

namespace Domain.HuntEquipmentPlan
{
    public interface IHuntEquipmentPlanService
    {
        HuntEquipmentPlanBaseViewModel GetHuntEquipmentPlanViewModel(int marketingYearId);
    }
}