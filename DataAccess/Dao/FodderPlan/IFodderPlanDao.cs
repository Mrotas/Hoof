using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.FodderPlan
{
    public interface IFodderPlanDao
    {
        IList<FodderPlanDto> GetFodderPlan(int marketingYearId);
    }
}