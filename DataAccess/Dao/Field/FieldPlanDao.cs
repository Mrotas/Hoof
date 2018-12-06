using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Dao.Field
{
    public class FieldPlanDao : DaoBase, IFieldPlanDao
    {
        public List<FieldPlan> GetFieldPlan(int year)
        {
            var fieldPlan = new List<FieldPlan>();
            using (var db = new DbContext())
            {
                fieldPlan = db.FieldPlan.Where(x => x.Year == year).ToList();
            }

            return fieldPlan;
        }
    }
}
