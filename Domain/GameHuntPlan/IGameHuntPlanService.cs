using Domain.GameHuntPlan.ViewModels;

namespace Domain.GameHuntPlan
{
    public interface IGameHuntPlanService
    {
        HuntPlanViewModel GetHuntPlanViewModel(int marketingYearId);
        void AddGameHuntPlan(GameHuntPlanViewModel model, int marketingYearId);
        void UpdateGameHuntPlan(GameHuntPlanViewModel model, int marketingYearId);
        void DeleteGameHuntPlan(int id);
    }
}