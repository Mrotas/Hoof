using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.CostPlan;
using Domain.CostPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class CostPlanController : Controller
    {
        private readonly ICostPlanService _costPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public CostPlanController() : this(new CostPlanService(), new MarketingYearService())
        {
        }

        public CostPlanController(ICostPlanService costPlanService, IMarketingYearService marketingYearService)
        {
            _costPlanService = costPlanService;
            _marketingYearService = marketingYearService;
        }
        
        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
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