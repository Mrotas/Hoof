using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.CarcassRevenue;
using Domain.CarcassRevenue.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class CarcassRevenueController : Controller
    {
        private readonly ICarcassRevenueService _carcassRevenueService;
        private readonly IMarketingYearService _marketingYearService;

        public CarcassRevenueController() : this(new CarcassRevenueService(), new MarketingYearService())
        {
        }

        public CarcassRevenueController(ICarcassRevenueService carcassRevenueService, IMarketingYearService marketingYearService)
        {
            _carcassRevenueService = carcassRevenueService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            CarcassRevenueBaseViewModel carcassRevenueBaseViewModel = _carcassRevenueService.GetCarcassRevenueViewModel(marketingYearId);
            return View(carcassRevenueBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(CarcassRevenueViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _carcassRevenueService.AddCarcassRevenue(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CarcassRevenueViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _carcassRevenueService.UpdateCarcassRevenue(model, marketingYearId);
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
                _carcassRevenueService.DeleteCarcassRevenue(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}