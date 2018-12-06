using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.Employee
{
    public interface IEmployeePlanDao
    {
        List<EmployeePlan> GetEmployeePlan(int year);
    }
}