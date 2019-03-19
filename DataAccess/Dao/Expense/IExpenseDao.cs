using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Expense
{
    public interface IExpenseDao
    {
        IList<ExpenseDto> GetByMarketingYear(int marketingYearId);
        void Insert(ExpenseDto dto);
        void Update(ExpenseDto dto);
        void Delete(int id);
    }
}