using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.FodderPlan
{
    public interface IFodderPlanDao
    {
        IList<FodderPlanDto> GetByMarketingYear(int marketingYearId);
        void Insert(FodderPlanDto dto);
        void Update(FodderPlanDto dto);
        void Delete(int fodderType, int marketingYearId);
    }
}