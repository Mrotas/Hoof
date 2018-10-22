using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.EstimatedGameCount30March
{
    public class EstimatedGameCount30MarchPlanDao : DaoBase, IEstimatedGameCount30MarchPlanDao
    {
        public List<EstimatedGameCount30MarchPlan> GetEstimatedGameCount30MarchPlan(int year)
        {
            var estimatedGameCount30MarchPlan = new List<EstimatedGameCount30MarchPlan>();
            using (DbContext)
            {
                estimatedGameCount30MarchPlan = DbContext.EstimatedGameCount30MarchPlan.Where(x => x.Year == year).ToList();
            }

            return estimatedGameCount30MarchPlan;
        }
    }
}
