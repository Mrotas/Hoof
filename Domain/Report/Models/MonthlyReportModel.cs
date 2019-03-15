using System;
using Domain.MarketingYear.Models;
using Domain.Report.Models.Fodder;

namespace Domain.Report.Models
{
    public class MonthlyReportModel
    {
        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }
        public MarketingYearModel MarketingYearModel { get; set; }
        public MonthlyReportGameModel MonthlyReportBigGameModel { get; set; }
        public MonthlyReportGameModel MonthlyReportSmallGameModel { get; set; }
        public MonthlyReportFodderModel MonthlyReportFodderModel { get; set; }
    }
}
