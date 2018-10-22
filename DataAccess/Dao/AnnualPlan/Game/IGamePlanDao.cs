using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Game
{
    public interface IGamePlanDao
    {
        List<GamePlan> GetGamePlan(int year);
    }
}