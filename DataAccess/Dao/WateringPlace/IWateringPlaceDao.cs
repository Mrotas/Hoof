using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.WateringPlace
{
    public interface IWateringPlaceDao
    {
        IList<WateringPlaceDto> GetActiveByMarketingYear(int marketingYearId);
    }
}