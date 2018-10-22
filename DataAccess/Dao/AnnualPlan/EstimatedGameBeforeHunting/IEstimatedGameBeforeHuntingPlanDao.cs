using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.EstimatedGameBeforeHunting
{
    public interface IEstimatedGameBeforeHuntingPlanDao
    {
        List<EstimatedGameBeforeHuntPeriodPlan> GetEstimatedGameBeforeHuntingPeriodPlan(int year);
    }
}