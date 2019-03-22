using System;
using Domain.Report.ViewModels;

namespace Domain.Report
{
    public interface IReportService
    {
        MonthlyReportViewModel GetMonthlyReportViewModel(DateTime startDate, DateTime endDate);
    }
}