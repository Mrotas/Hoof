using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.CostPlan
{
    public interface ICostPlanDao
    {
        IList<CostPlanDto> GetCostPlan(int marketingYearId);
    }
}