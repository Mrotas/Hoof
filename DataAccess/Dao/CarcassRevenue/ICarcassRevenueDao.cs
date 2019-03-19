using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.CarcassRevenue
{
    public interface ICarcassRevenueDao
    {
        IList<CarcassRevenueDto> GetByMarketingYear(int marketingYearId);
        void Insert(CarcassRevenueDto dto);
        void Update(CarcassRevenueDto dto);
        void Delete(int id);
    }
}