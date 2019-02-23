using Domain.FodderPlan.ViewModels;

namespace Domain.FodderPlan
{
    public interface IFodderPlanService
    {
        FodderPlanBaseViewModel GetFodderPlanViewModel(int marketingYearId);
    }
}