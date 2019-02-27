using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.EmploymentPlan
{
    public interface IEmploymentPlanDao
    {
        IList<EmploymentPlanDto> GetByMarketingYear(int marketingYearId);
        void Insert(EmploymentPlanDto dto);
        void Update(EmploymentPlanDto dto);
        void Delete(int employmentType, int marketingYearId);
    }
}