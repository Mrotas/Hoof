using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.Fodder
{
    public interface IFodderPlanDao
    {
        List<FodderPlan> GetFodderPlan(int year);
    }
}