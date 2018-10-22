using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Field
{
    public interface IFieldPlanDao
    {
        List<FieldPlan> GetFieldPlan(int year);
    }
}