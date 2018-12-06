using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Dao.Fodder
{
    public class FodderPlanDao : DaoBase, IFodderPlanDao
    {
        public List<FodderPlan> GetFodderPlan(int year)
        {
            var fodderPlan = new List<FodderPlan>();
            using (var db = new DbContext())
            {
                fodderPlan = db.FodderPlan.Where(x => x.Year == year).ToList();
            }

            return fodderPlan;
        }
    }
}
