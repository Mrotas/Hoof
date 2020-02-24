using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Labor;
using Domain.Labor.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class LaborController : Controller
    {
        private readonly ILaborService _laborService;
        private readonly IMarketingYearService _marketingYearService;

        public LaborController() : this(new LaborService(), new MarketingYearService())
        {
        }

        public LaborController(ILaborService laborService, IMarketingYearService marketingYearService)
        {
            _laborService = laborService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            LaborBaseViewModel laborBaseViewModel = _laborService.GetLaborViewModel(marketingYearId);
            return View(laborBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(LaborViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _laborService.AddLabor(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(LaborViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _laborService.UpdateLabor(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = String.Empty;
            try
            {
                _laborService.DeleteLabor(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}