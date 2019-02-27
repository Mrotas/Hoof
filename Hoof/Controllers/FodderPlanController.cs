using System;
using System.Web.Mvc;
using Domain.FodderPlan;
using Domain.FodderPlan.ViewModels;

namespace Hoof.Controllers
{
    public class FodderPlanController : Controller
    {
        private readonly IFodderPlanService _fodderPlanService;

        public FodderPlanController() : this(new FodderPlanService())
        {
        }

        public FodderPlanController(IFodderPlanService fodderPlanService)
        {
            _fodderPlanService = fodderPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            FodderPlanBaseViewModel fodderPlanViewBaseModel = _fodderPlanService.GetFodderPlanViewModel(marketingYearId);
            return View(fodderPlanViewBaseModel);
        }

        [HttpPost]
        public JsonResult Add(FodderPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderPlanService.AddFodderPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(FodderPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderPlanService.UpdateFodderPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int fodderType, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderPlanService.DeleteFodderPlan(fodderType, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}