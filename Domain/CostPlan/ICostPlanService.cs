using Domain.CostPlan.ViewModels;

namespace Domain.CostPlan
{
    public interface ICostPlanService
    {
        CostPlanBaseViewModel GetCostPlanViewModel(int marketingYearId);
        void AddCostPlan(CostPlanViewModel model, int marketingYearId);
        void UpdateCostPlan(CostPlanViewModel model, int marketingYearId);
        void DeleteCostPlan(int costType, int marketingYearId);
    }
}