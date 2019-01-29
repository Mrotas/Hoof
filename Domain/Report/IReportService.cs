using System;
using System.Collections.Generic;
using Domain.Report.Models;

namespace Domain.Report
{
    public interface IReportService
    {
        List<MonthlyReportModel> GetMonthlyReportData(DateTime startDate, DateTime endDate);
    }
}