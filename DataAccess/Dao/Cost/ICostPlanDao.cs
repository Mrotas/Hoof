using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.Cost
{
    public interface ICostPlanDao
    {
        List<CostPlan> GetCostPlan(int year);
    }
}