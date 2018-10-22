using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.EstimatedGameBeforeHunting
{
    public class EstimatedGameBeforeHuntingPlanDao : DaoBase, IEstimatedGameBeforeHuntingPlanDao
    {
        public List<EstimatedGameBeforeHuntPeriodPlan> GetEstimatedGameBeforeHuntingPeriodPlan(int year)
        {
            var estimatedGameBeforeHuntPeriodPlan = new List<EstimatedGameBeforeHuntPeriodPlan>();
            using (DbContext)
            {
                estimatedGameBeforeHuntPeriodPlan = DbContext.EstimatedGameBeforeHuntPeriodPlan.Where(x => x.Year == year).ToList();
            }

            return estimatedGameBeforeHuntPeriodPlan;
        }
    }
}
