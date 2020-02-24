using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Pasture;
using Domain.Pasture.ViewModels;

namespace Hoof.Controllers
{
    public class PastureController : Controller
    {
        private readonly IPastureService _pastureService;
        private readonly IMarketingYearService _marketingYearService;

        public PastureController() : this(new PastureService(), new MarketingYearService())
        {
        }

        public PastureController(IPastureService pastureService, IMarketingYearService marketingYearService)
        {
            _pastureService = pastureService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            PastureBaseViewModel pastureBaseViewModel = _pastureService.GetPastureViewModel(marketingYearId);
            return View(pastureBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(PastureViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pastureService.AddPasture(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(PastureViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pastureService.UpdatePasture(model, marketingYearId);
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
                _pastureService.DeletePasture(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}