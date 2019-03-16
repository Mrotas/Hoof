using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Pasture
{
    public interface IPastureDao
    {
        IList<PastureDto> GetByMarketingYear(int marketingYearId);
    }
}