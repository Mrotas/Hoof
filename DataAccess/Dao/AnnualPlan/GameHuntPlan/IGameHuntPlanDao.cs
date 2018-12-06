using System.Collections.Generic;

namespace DataAccess.Dao.AnnualPlan.GameHuntPlan
{
    public interface IGameHuntPlanDao
    {
        List<Entities.AnnualPlan.GameHuntPlan> GetAll();
        List<Entities.AnnualPlan.GameHuntPlan> GetGameHuntPlan(int year);
    }
}