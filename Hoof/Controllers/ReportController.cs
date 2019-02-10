using System;
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

        public ActionResult MonthlyReport(DateTime dateFrom, DateTime dateTo)
        {
            MonthlyReportModel monthlyReportsModel = _reportService.GetMonthlyReportData(dateFrom, dateTo);

            monthlyReportsModel.ReportDateFrom = dateFrom;
            monthlyReportsModel.ReportDateTo = dateTo;

            return View(monthlyReportsModel);
        }
    }
}