using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.HuntEquipmentPlan;
using Domain.HuntEquipmentPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class HuntEquipmentPlanController : Controller
    {
        private readonly IHuntEquipmentPlanService _huntEquipmentPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public HuntEquipmentPlanController(): this(new HuntEquipmentPlanService(), new MarketingYearService())
        {
        }

        public HuntEquipmentPlanController(IHuntEquipmentPlanService huntEquipmentPlanService, IMarketingYearService marketingYearService)
        {
            _huntEquipmentPlanService = huntEquipmentPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult Plan(int marketingYearId)
        {
            HuntEquipmentPlanBaseViewModel huntEquipmentPlanBaseViewModel = _huntEquipmentPlanService.GetHuntEquipmentPlanViewModel(marketingYearId);
            return View(huntEquipmentPlanBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(HuntEquipmentPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _huntEquipmentPlanService.AddHuntEquipmentPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message } , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(HuntEquipmentPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _huntEquipmentPlanService.UpdateHuntEquipmentPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int huntEquipmentType, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _huntEquipmentPlanService.DeleteHuntEquipmentPlan(huntEquipmentType, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}