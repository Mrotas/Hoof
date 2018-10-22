using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Cost
{
    public class CostPlanDao : DaoBase, ICostPlanDao
    {
        public List<CostPlan> GetCostPlan(int year)
        {
            var costPlan = new List<CostPlan>();
            using (var db = new DbContext())
            {
                costPlan = db.CostPlan.Where(x => x.Year == year).ToList();
            }

            return costPlan;
        }
    }
}
