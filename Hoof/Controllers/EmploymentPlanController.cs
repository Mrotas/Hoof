using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.EmploymentPlan;
using Domain.EmploymentPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class EmploymentPlanController : Controller
    {
        private readonly IEmploymentPlanService _employmentPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public EmploymentPlanController() : this(new EmploymentPlanService(), new MarketingYearService())
        {
        }

        public EmploymentPlanController(IEmploymentPlanService employmentPlanService, IMarketingYearService marketingYearService)
        {
            _employmentPlanService = employmentPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult Plan(int marketingYearId)
        {
            EmploymentPlanBaseViewModel employmentPlanBaseViewModel = _employmentPlanService.GetEmploymentPlanViewModel(marketingYearId);
            return View(employmentPlanBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(EmploymentPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _employmentPlanService.AddEmploymentPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(EmploymentPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _employmentPlanService.UpdateEmploymentPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int employmentType, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _employmentPlanService.DeleteEmploymentPlan(employmentType, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}