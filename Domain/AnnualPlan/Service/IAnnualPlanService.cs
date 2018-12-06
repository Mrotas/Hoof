using Domain.AnnualPlan.ViewModels;

namespace Domain.AnnualPlan.Service
{
    public interface IAnnualPlanService
    {
        AnnualPlanViewModel GetAnnualPlanViewModel();
    }
}