using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.TrunkFood
{
    public interface ITrunkFoodPlanDao
    {
        List<TrunkFoodPlan> GetTrunkFoodPlan(int year);
    }
}