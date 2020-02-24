using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Expense;
using DataAccess.Dto;
using Domain.AnnualPlanStatus;
using Domain.AnnualPlanStatus.Models;
using Domain.Expense.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseDao _expenseDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public ExpenseService() : this(new ExpenseDao(), new MarketingYearService(), new AnnualPlanStatusService())
        {
        }

        public ExpenseService(IExpenseDao expenseDao, IMarketingYearService marketingYearService, IAnnualPlanStatusService annualPlanStatusService)
        {
            _expenseDao = expenseDao;
            _marketingYearService = marketingYearService;
            _annualPlanStatusService = annualPlanStatusService;
        }

        public ExpenseBaseViewModel GetExpenseBaseViewModel(int marketingYearId)
        {
            IList<ExpenseDto> expenseDtos = _expenseDao.GetByMarketingYear(marketingYearId);

            List<ExpenseViewModel> expenseViewModels = expenseDtos.Select(x => new ExpenseViewModel
            {
                Id = x.Id,
                Cost = x.Cost,
                Description = x.Description,
                Note = x.Note,
                Date = x.Date
            }).ToList();

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);
            AnnualPlanStatusModel annualPlanStatusModel = _annualPlanStatusService.GetByMarketingYearId(marketingYearId);

            var expenseBaseViewModel = new ExpenseBaseViewModel
            {
                ExpenseViewModels = expenseViewModels,
                MarketingYearModel = marketingYearModel,
                AnnualPlanStatusModel = annualPlanStatusModel
            };

            return expenseBaseViewModel;
        }

        public void AddExpense(ExpenseViewModel model, int marketingYearId)
        {
            if (String.IsNullOrWhiteSpace(model.Description))
            {
                throw new Exception("Nie można dodać wydatku koła łowieckiego");
            }
            
            var dto = new ExpenseDto
            {
                Cost = model.Cost,
                Description = model.Description,
                Note = model.Note,
                Date = model.Date
            };

            _expenseDao.Insert(dto);
        }

        public void UpdateExpense(ExpenseViewModel model, int marketingYearId)
        {
            if (String.IsNullOrWhiteSpace(model.Description))
            {
                throw new Exception("Nie można edytować wydatku koła łowieckiego");
            }

            var dto = new ExpenseDto
            {
                Id = model.Id,
                Cost = model.Cost,
                Description = model.Description,
                Note = model.Note,
                Date = model.Date
            };

            _expenseDao.Update(dto);
        }

        public void DeleteExpense(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Nie można usunąć wydatku koła łowieckiego");
            }

            _expenseDao.Delete(id);
        }
    }
}
