using System;
using System.Web.Mvc;
using Domain.TrunkFoodPlan;
using Domain.TrunkFoodPlan.Models;
using Domain.TrunkFoodPlan.ViewModels;

namespace Hoof.Controllers
{
    public class TrunkFoodPlanController : Controller
    {
        private readonly ITrunkFoodPlanService _trunkFoodPlanService;

        public TrunkFoodPlanController() : this(new TrunkFoodPlanService())
        {
        }

        public TrunkFoodPlanController(ITrunkFoodPlanService trunkFoodPlanService)
        {
            _trunkFoodPlanService = trunkFoodPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            TrunkFoodPlanViewModel trunkFoodPlanViewModel = _trunkFoodPlanService.GetTrunkFoodPlanViewModel(marketingYearId);
            return View(trunkFoodPlanViewModel);
        }

        [HttpPost]
        public JsonResult Add(TrunkFoodPlanModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _trunkFoodPlanService.AddTrunkFoodPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(TrunkFoodPlanModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _trunkFoodPlanService.UpdateTrunkFoodPlan(model, marketingYearId);
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
                _trunkFoodPlanService.DeleteTrunkFoodPlan(marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}