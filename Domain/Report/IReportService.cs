using System;
using Domain.Report.Models;

namespace Domain.Report
{
    public interface IReportService
    {
        MonthlyReportModel GetMonthlyReportData(DateTime startDate, DateTime endDate);
    }
}