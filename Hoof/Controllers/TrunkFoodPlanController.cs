using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.TrunkFoodPlan;
using Domain.TrunkFoodPlan.Models;
using Domain.TrunkFoodPlan.ViewModels;

namespace Hoof.Controllers
{
    public class TrunkFoodPlanController : Controller
    {
        private readonly ITrunkFoodPlanService _trunkFoodPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public TrunkFoodPlanController() : this(new TrunkFoodPlanService(), new MarketingYearService())
        {
        }

        public TrunkFoodPlanController(ITrunkFoodPlanService trunkFoodPlanService, IMarketingYearService marketingYearService)
        {
            _trunkFoodPlanService = trunkFoodPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
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