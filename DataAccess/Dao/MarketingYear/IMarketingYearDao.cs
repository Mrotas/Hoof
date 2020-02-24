using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.MarketingYear
{
    public interface IMarketingYearDao
    {
        IList<MarketingYearDto> GetAll();
        MarketingYearDto GetCurrent();
        MarketingYearDto GetById(int marketingYearId);
        int Insert(MarketingYearDto newMarketingYear);
    }
}