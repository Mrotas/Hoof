using System;
using System.Web.Mvc;
using Domain.Report;
using Domain.Report.Models;
using Domain.Report.ViewModels;

namespace Hoof.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController() : this(new ReportService())
        {
            
        }

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            return View();
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