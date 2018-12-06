using System.Collections.Generic;
using Domain.AnnualPlan.Models.GamePlan;

namespace Domain.AnnualPlan.Service.GamePlan
{
    public interface IGamePlanService
    {
        IList<GamePlanModel> GetGamePlanModels(int year);
    }
}