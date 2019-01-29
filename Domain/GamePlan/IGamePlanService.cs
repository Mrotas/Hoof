using System.Collections.Generic;
using Domain.AnnualPlan.Models.GamePlan;

namespace Domain.GamePlan
{
    public interface IGamePlanService
    {
        IList<GamePlanModel> GetGamePlanModels(int marketingYear);
    }
}