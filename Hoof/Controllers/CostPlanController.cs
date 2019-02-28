using System;
using System.Web.Mvc;
using Domain.CostPlan;
using Domain.CostPlan.ViewModels;

namespace Hoof.Controllers
{
    public class CostPlanController : Controller
    {
        private readonly ICostPlanService _costPlanService;

        public CostPlanController() : this(new CostPlanService())
        {
        }

        public CostPlanController(ICostPlanService costPlanService)
        {
            _costPlanService = costPlanService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            CostPlanBaseViewModel costPlanBaseViewModel = _costPlanService.GetCostPlanViewModel(marketingYearId);
            return View(costPlanBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(CostPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _costPlanService.AddCostPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CostPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _costPlanService.UpdateCostPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int costType, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _costPlanService.DeleteCostPlan(costType, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}