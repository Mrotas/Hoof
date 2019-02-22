using Domain.GameCountPlan.ViewModels;

namespace Domain.GameCountPlan
{
    public interface IGameCountPlanService
    {
        CountPlanViewModel GetCountPlanViewModel(int marketingYearId);
    }
}