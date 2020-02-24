using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Fodder;
using Domain.Fodder.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class FodderController : Controller
    {
        private readonly IFodderService _fodderService;
        private readonly IMarketingYearService _marketingYearService;

        public FodderController() : this(new FodderService(), new MarketingYearService())
        {
        }

        public FodderController(IFodderService fodderService, IMarketingYearService marketingYearService)
        {
            _fodderService = fodderService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            FodderBaseViewModel fodderBaseView = _fodderService.GetFodderViewModel(marketingYearId);
            return View(fodderBaseView);
        }
        
        [HttpPost]
        public JsonResult Add(FodderViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderService.AddFodder(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(FodderViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderService.UpdateFodder(model, marketingYearId);
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
                _fodderService.DeleteFodder(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}