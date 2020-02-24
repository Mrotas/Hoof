using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Expense;
using Domain.Expense.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IMarketingYearService _marketingYearService;

        public ExpenseController() : this(new ExpenseService(), new MarketingYearService())
        {
        }

        public ExpenseController(IExpenseService expenseService, IMarketingYearService marketingYearService)
        {
            _expenseService = expenseService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            ExpenseBaseViewModel pastureBaseViewModel = _expenseService.GetExpenseBaseViewModel(marketingYearId);
            return View(pastureBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(ExpenseViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _expenseService.AddExpense(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(ExpenseViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _expenseService.UpdateExpense(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = String.Empty;
            try
            {
                _expenseService.DeleteExpense(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}