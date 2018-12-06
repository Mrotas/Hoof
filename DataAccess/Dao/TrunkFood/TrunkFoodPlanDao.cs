using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Dao.TrunkFood
{
    public class TrunkFoodPlanDao : DaoBase, ITrunkFoodPlanDao
    {
        public List<TrunkFoodPlan> GetTrunkFoodPlan(int year)
        {
            var trunkFoodPlan = new List<TrunkFoodPlan>();
            using (var db = new DbContext())
            {
                trunkFoodPlan = db.TrunkFoodPlan.Where(x => x.Year == year).ToList();
            }

            return trunkFoodPlan;
        }
    }
}
