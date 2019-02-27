using Domain.EmploymentPlan.ViewModels;

namespace Domain.EmploymentPlan
{
    public interface IEmploymentPlanService
    {
        EmploymentPlanBaseViewModel GetEmploymentPlanViewModel(int marketingYearId);
        void AddEmploymentPlan(EmploymentPlanViewModel model, int marketingYearId);
        void UpdateEmploymentPlan(EmploymentPlanViewModel model, int marketingYearId);
        void DeleteEmploymentPlan(int employmentType, int marketingYearId);
    }
}