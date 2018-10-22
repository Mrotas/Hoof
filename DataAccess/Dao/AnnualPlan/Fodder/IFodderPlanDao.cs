using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Fodder
{
    public interface IFodderPlanDao
    {
        List<FodderPlan> GetFodderPlan(int year);
    }
}