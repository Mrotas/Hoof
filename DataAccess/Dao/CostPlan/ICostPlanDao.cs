using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.CostPlan
{
    public interface ICostPlanDao
    {
        IList<CostPlanDto> GetByMarketingYear(int marketingYearId);
        void Insert(CostPlanDto dto);
        void Update(CostPlanDto dto);
        void Delete(int costType, int marketingYearId);
    }
}