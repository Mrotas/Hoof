using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Cost
{
    public interface ICostPlanDao
    {
        List<CostPlan> GetCostPlan(int year);
    }
}