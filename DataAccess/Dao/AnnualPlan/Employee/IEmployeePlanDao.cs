using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Employee
{
    public interface IEmployeePlanDao
    {
        List<EmployeePlan> GetEmployeePlan(int year);
    }
}