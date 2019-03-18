using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.WateringPlace
{
    public interface IWateringPlaceDao
    {
        IList<WateringPlaceDto> GetByMarketingYear(int marketingYearId);
        IList<WateringPlaceDto> GetActiveByMarketingYear(int marketingYearId);
        void Insert(WateringPlaceDto dto);
        void Update(WateringPlaceDto dto);
        void Delete(int id);
    }
}