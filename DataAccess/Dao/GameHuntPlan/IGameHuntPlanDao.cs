using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.GameHuntPlan
{
    public interface IGameHuntPlanDao
    {
        IList<GameHuntPlanDto> GetAll();
        IList<GameHuntPlanDto> GetGameHuntPlan(int marketingYearId);
    }
}