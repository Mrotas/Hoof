using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Report;
using Domain.Report.Models;

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

        public JsonResult GetMonthlyReportData(DateTime startDate, DateTime endDate)
        {
            List<MonthlyReportModel> monthlyReportsModels = _reportService.GetMonthlyReportData(startDate, endDate);

            return Json(monthlyReportsModels, JsonRequestBehavior.AllowGet);
        }
    }
}