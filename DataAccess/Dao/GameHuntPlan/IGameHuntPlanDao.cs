using System.Collections.Generic;

namespace DataAccess.Dao.GameHuntPlan
{
    public interface IGameHuntPlanDao
    {
        List<Entities.GameHuntPlan> GetAll();
        List<Entities.GameHuntPlan> GetGameHuntPlan(int year);
    }
}