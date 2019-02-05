using System;
using Domain.MarketingYear.Models;

namespace Domain.MarketingYear
{
    public interface IMarketingYearService
    {
        int GetMarketingYearByDate(DateTime date);
        MarketingYearModel GetMarketingYearModel(int marketingYearId);
        bool IsDateInMarketingYear(DateTime date, int marketingYearId);
    }
}