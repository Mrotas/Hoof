using Domain.TrunkFoodPlan.Models;
using Domain.TrunkFoodPlan.ViewModels;

namespace Domain.TrunkFoodPlan
{
    public interface ITrunkFoodPlanService
    {
        TrunkFoodPlanViewModel GetTrunkFoodPlanViewModel(int marketingYearId);
        void AddTrunkFoodPlan(TrunkFoodPlanModel model, int marketingYearId);
        void UpdateTrunkFoodPlan(TrunkFoodPlanModel model, int marketingYearId);
        void DeleteTrunkFoodPlan(int marketingYearId);
    }
}