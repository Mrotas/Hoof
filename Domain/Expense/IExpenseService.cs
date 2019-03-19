using Domain.Expense.ViewModels;

namespace Domain.Expense
{
    public interface IExpenseService
    {
        ExpenseBaseViewModel GetExpenseBaseViewModel(int marketingYearId);
        void AddExpense(ExpenseViewModel model, int marketingYearId);
        void UpdateExpense(ExpenseViewModel model, int marketingYearId);
        void DeleteExpense(int id);
    }
}