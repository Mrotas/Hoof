using System;
using System.Collections.Generic;
using Domain.MarketingYear.Models;

namespace Domain.MarketingYear
{
    public interface IMarketingYearService
    {
        IList<MarketingYearModel> GetAll();
        int GetMarketingYearByDate(DateTime date);
        MarketingYearModel GetCurrentMarketingYearModel();
        MarketingYearModel GetMarketingYearModel(int marketingYearId);
        bool IsDateInMarketingYear(DateTime date, int marketingYearId);
        int Create();
    }
}