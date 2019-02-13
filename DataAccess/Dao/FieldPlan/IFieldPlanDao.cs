using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.FieldPlan
{
    public interface IFieldPlanDao
    {
        IList<FieldPlanDto> GetByMarketingYear(int marketingYearId);
    }
}