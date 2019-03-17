using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Pulpit
{
    public interface IPulpitDao
    {
        IList<PulpitDto> GetByMarketingYear(int marketingYearId);
        void Insert(PulpitDto dto);
        void Update(PulpitDto dto);
        void Delete(int id);
    }
}