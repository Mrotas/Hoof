using System;

namespace Domain.MarketingYear
{
    public interface IMarketingYearService
    {
        int GetMarketingYearByDate(DateTime date);
        bool IsDateInMarketingYear(DateTime date, int marketingYearId);
    }
}