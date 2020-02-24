using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.FieldPlan;
using Domain.FieldPlan.Models;
using Domain.FieldPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class FieldPlanController : Controller
    {
        private readonly IFieldPlanService _fieldPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public FieldPlanController() : this(new FieldPlanService(), new MarketingYearService())
        {
        }

        public FieldPlanController(IFieldPlanService fieldPlanService, IMarketingYearService marketingYearService)
        {
            _fieldPlanService = fieldPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult Plan(int marketingYearId)
        {
            FieldPlanViewModel filedPlanModel = _fieldPlanService.GetFieldPlanViewModel(marketingYearId);
            return View(filedPlanModel);
        }

        [HttpPost]
        public JsonResult Add(FiledPlanModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fieldPlanService.AddFieldPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(FiledPlanModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fieldPlanService.UpdateFieldPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fieldPlanService.DeleteFieldPlan(marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}