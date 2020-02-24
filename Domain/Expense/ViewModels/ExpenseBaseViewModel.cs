using System.Collections.Generic;
using Domain.AnnualPlanStatus.Models;
using Domain.MarketingYear.Models;

namespace Domain.Expense.ViewModels
{
    public class ExpenseBaseViewModel
    {
        public MarketingYearModel MarketingYearModel { get; set; }
        public AnnualPlanStatusModel AnnualPlanStatusModel { get; set; }
        public IList<ExpenseViewModel> ExpenseViewModels { get; set; }
    }
}
