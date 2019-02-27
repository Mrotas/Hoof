using Domain.FodderPlan.ViewModels;

namespace Domain.FodderPlan
{
    public interface IFodderPlanService
    {
        FodderPlanBaseViewModel GetFodderPlanViewModel(int marketingYearId);
        void AddFodderPlan(FodderPlanViewModel model, int marketingYearId);
        void UpdateFodderPlan(FodderPlanViewModel model, int marketingYearId);
        void DeleteFodderPlan(int fodderType, int marketingYearId);
    }
}