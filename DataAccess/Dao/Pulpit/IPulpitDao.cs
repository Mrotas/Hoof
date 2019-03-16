using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Pulpit
{
    public interface IPulpitDao
    {
        IList<PulpitDto> GetByMarketingYear(int marketingYearId);
    }
}