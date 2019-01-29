using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.TrunkFoodPlan
{
    public interface ITrunkFoodPlanDao
    {
        IList<TrunkFoodPlanDto> GetTrunkFoodPlan(int marketingYearId);
    }
}