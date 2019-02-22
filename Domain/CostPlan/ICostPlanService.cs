using Domain.CostPlan.ViewModels;

namespace Domain.CostPlan
{
    public interface ICostPlanService
    {
        CostPlanBaseViewModel GetCostPlanViewModel(int marketingYearId);
    }
}