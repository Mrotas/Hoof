using System;
using System.Web.Mvc;
using Domain.HuntEquipmentPlan;
using Domain.HuntEquipmentPlan.ViewModels;

namespace Hoof.Controllers
{
    public class HuntEquipmentPlanController : Controller
    {
        private readonly IHuntEquipmentPlanService _huntEquipmentPlanService;

        public HuntEquipmentPlanController(): this(new HuntEquipmentPlanService())
        {
        }

        public HuntEquipmentPlanController(IHuntEquipmentPlanService huntEquipmentPlanService)
        {
            _huntEquipmentPlanService = huntEquipmentPlanService;
        }

        public ActionResult Index()
        {
            return View();
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
    }
}