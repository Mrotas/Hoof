using System.Collections.Generic;
using Domain.GameHuntPlan.ViewModels;

namespace Domain.GameHuntPlan
{
    public interface IGameHuntPlanService
    {
        HuntPlanViewModel GetHuntPlanViewModel(int marketingYearId);
    }
}