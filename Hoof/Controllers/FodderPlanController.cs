using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.FodderPlan;
using Domain.FodderPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class FodderPlanController : Controller
    {
        private readonly IFodderPlanService _fodderPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public FodderPlanController() : this(new FodderPlanService(), new MarketingYearService())
        {
        }

        public FodderPlanController(IFodderPlanService fodderPlanService, IMarketingYearService marketingYearService)
        {
            _fodderPlanService = fodderPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
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