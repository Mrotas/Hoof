using System;
using Domain.AnnualPlan.ViewModels;

namespace Domain.AnnualPlan
{
    public interface IAnnualPlanService
    {
        AnnualPlanViewModel GetAnnualPlanViewModel(int marketingYearId);
    }
}