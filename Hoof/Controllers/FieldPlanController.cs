using System;
using System.Web.Mvc;
using Domain.FieldPlan;
using Domain.FieldPlan.Models;
using Domain.FieldPlan.ViewModels;

namespace Hoof.Controllers
{
    public class FieldPlanController : Controller
    {
        private readonly IFieldPlanService _fieldPlanService;

        public FieldPlanController() : this(new FieldPlanService())
        {
        }

        public FieldPlanController(IFieldPlanService fieldPlanService)
        {
            _fieldPlanService = fieldPlanService;
        }

        public ActionResult Index()
        {
            return View();
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