using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.TrunkFood
{
    public interface ITrunkFoodPlanDao
    {
        List<TrunkFoodPlan> GetTrunkFoodPlan(int year);
    }
}