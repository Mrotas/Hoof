using Domain.EmploymentPlan.ViewModels;

namespace Domain.EmploymentPlan
{
    public interface IEmploymentPlanService
    {
        EmploymentPlanBaseViewModel GetEmploymentPlanViewModel(int marketingYearId);
    }
}