using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities;

namespace DataAccess.Dao.Employee
{
    public class EmployeePlanDao : DaoBase, IEmployeePlanDao
    {
        public List<EmployeePlan> GetEmployeePlan(int year)
        {
            var eployeePlan = new List<EmployeePlan>();
            using (var db = new DbContext())
            {
                eployeePlan = db.EmployeePlan.Where(x => x.Year == year).ToList();
            }

            return eployeePlan;
        }
    }
}
