using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.GameCountBeforeHuntingSeason
{
    public interface IGameCountBeforeHuntingSeasonPlanDao
    {
        List<GameCountBeforeHuntingSeasonPlan> GetGameCountBeforeHuntingSeasonPlan(int year);
    }
}