using System;
using System.Web.Mvc;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Report;
using Domain.Report.ViewModels;

namespace Hoof.Controllers
{
    public class ReportController : Controller
    {
        private readonly IMarketingYearService _marketingYearService;
        private readonly IReportService _reportService;

        public ReportController() : this(new ReportService(), new MarketingYearService())
        {
            
        }

        public ReportController(IReportService reportService, IMarketingYearService marketingYearService)
        {
            _reportService = reportService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            MarketingYearModel currentMarketingYearModel = _marketingYearService.GetCurrentMarketingYearModel();
            return View(currentMarketingYearModel);
        }

        public ActionResult MonthlyReport(DateTime dateFrom, DateTime dateTo)
        {
            MonthlyReportViewModel monthlyReportsModel = _reportService.GetMonthlyReportViewModel(dateFrom, dateTo);
            return View(monthlyReportsModel);
        }

        public JsonResult GetMonthlyReportJsonData(DateTime dateFrom, DateTime dateTo)
        {
            MonthlyReportViewModel monthlyReportsModel = _reportService.GetMonthlyReportViewModel(dateFrom, dateTo);
            return Json(monthlyReportsModel, JsonRequestBehavior.AllowGet);
        }
    }
}