using Domain.FodderPlan.ViewModels;

namespace Domain.FodderPlan
{
    public interface IFodderPlanService
    {
        FodderPlanViewBaseModel GetFodderPlanViewModel(int marketingYearId);
    }
}