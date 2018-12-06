using System.Collections.Generic;
using Domain.AnnualPlan.GamePlan.Models;

namespace Domain.AnnualPlan.GamePlan
{
    public interface IGamePlanService
    {
        IList<GamePlanModel> GetGamePlanModels(int year);
    }
}