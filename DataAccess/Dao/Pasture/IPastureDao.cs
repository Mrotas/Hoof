using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Pasture
{
    public interface IPastureDao
    {
        IList<PastureDto> GetByMarketingYear(int marketingYearId);
        IList<PastureDto> GetActiveByMarketingYear(int marketingYearId);
        void Insert(PastureDto dto);
        void Update(PastureDto dto);
        void Delete(int id);
    }
}