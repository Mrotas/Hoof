using Common.Enums;
using Domain.AnnualPlan.Models.GamePlan;

namespace Domain.GamePlan
{
    public interface IGamePlanService
    {
        AnnualPlanGameModel GetGameAnnualPlanModel(GameType gameType, int marketingYearId);
    }
}