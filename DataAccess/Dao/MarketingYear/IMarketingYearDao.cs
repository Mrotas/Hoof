using System;
using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.MarketingYear
{
    public interface IMarketingYearDao
    {
        IList<MarketingYearDto> GetAll();
        Entities.MarketingYear GetById(int marketingYearId);
        int GetMarketingYearId(DateTime marketingYearStart, DateTime marketingYearEnd);
    }
}