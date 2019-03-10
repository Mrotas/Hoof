using Domain.GameCountPlan.ViewModels;

namespace Domain.GameCountPlan
{
    public interface IGameCountPlanService
    {
        CountPlanViewModel GetCountPlanViewModel(int marketingYearId);
        void AddGameHuntPlan(GameCountPlanViewModel model, int marketingYearId);
        void UpdateGameHuntPlan(GameCountPlanViewModel model, int marketingYearId);
        void DeleteGameHuntPlan(int id);
    }
}