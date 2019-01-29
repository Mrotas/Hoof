using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.EmploymentPlan
{
    public interface IEmploymentPlanDao
    {
        IList<EmploymentPlanDto> GetEmploymentPlan(int marketingYearId);
    }
}