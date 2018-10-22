using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.EstimatedGameCount30March
{
    public interface IEstimatedGameCount30MarchPlanDao
    {
        List<EstimatedGameCount30MarchPlan> GetEstimatedGameCount30MarchPlan(int year);
    }
}