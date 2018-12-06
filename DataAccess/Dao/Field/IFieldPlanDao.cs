using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.Field
{
    public interface IFieldPlanDao
    {
        List<FieldPlan> GetFieldPlan(int year);
    }
}