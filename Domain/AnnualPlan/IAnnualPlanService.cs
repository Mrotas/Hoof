using System;
using System.Web;
using Domain.AnnualPlan.ViewModels;

namespace Domain.AnnualPlan
{
    public interface IAnnualPlanService
    {
        AnnualPlanViewModel GetAnnualPlanViewModel(HttpCookie userId, int marketingYearId);
    }
}