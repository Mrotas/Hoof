using System;
using System.Web.Mvc;
using Domain.Expense;
using Domain.Expense.ViewModels;

namespace Hoof.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController() : this(new ExpenseService())
        {
        }

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public ActionResult Index()
        {
            return View();
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