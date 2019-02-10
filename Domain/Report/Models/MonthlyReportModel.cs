using System;

namespace Domain.Report.Models
{
    public class MonthlyReportModel
    {
        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }
        public MonthlyReportGameModel MonthlyReportBigGameModel { get; set; }
        public MonthlyReportGameModel MonthlyReportSmallGameModel { get; set; }
    }
}
