using System;
using Domain.MarketingYear.Models;
using Domain.Report.Models.Fodder;
using Domain.Report.Models.Game;
using Domain.Report.Models.Labor;

namespace Domain.Report.ViewModels
{
    public class MonthlyReportViewModel
    {
        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }
        public MarketingYearModel MarketingYearModel { get; set; }
        public MonthlyReportGameModel MonthlyReportBigGameModel { get; set; }
        public MonthlyReportGameModel MonthlyReportSmallGameModel { get; set; }
        public MonthlyReportFodderModel MonthlyReportFodderModel { get; set; }
        public MonthlyReportLaborModel MonthlyReportLaborModel { get; set; }
    }
}
