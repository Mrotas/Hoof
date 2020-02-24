using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.AnnualPlan;
using Domain.AnnualPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class AnnualPlanController : Controller
    {
        private readonly IAnnualPlanService _annualPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public AnnualPlanController() : this(new AnnualPlanService(), new MarketingYearService())
        {
        }

        public AnnualPlanController(IAnnualPlanService annualPlanService, IMarketingYearService marketingYearService)
        {
            _annualPlanService = annualPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            marketingYearModels.RemoveAt(0);
            return View(marketingYearModels);
        }

        public JsonResult Create()
        {
            int newMarketingYearId = _marketingYearService.Create();
            return Json(newMarketingYearId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Plan(int marketingYearId)
        {
            AnnualPlanViewModel annualPlanViewModel = _annualPlanService.GetAnnualPlanViewModel(Request.Cookies["User"], marketingYearId);
            return View(annualPlanViewModel);
        }

        public JsonResult GetAnnualPlanJsonData(int marketingYearId)
        {
            AnnualPlanViewModel annualPlanViewModel = _annualPlanService.GetAnnualPlanViewModel(Request.Cookies["User"], marketingYearId);
            return Json(annualPlanViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}