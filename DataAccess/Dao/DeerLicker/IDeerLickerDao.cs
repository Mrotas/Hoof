using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.DeerLicker
{
    public interface IDeerLickerDao
    {
        IList<DeerLickerDto> GetByMarketingYear(int marketingYearId);
        void Insert(DeerLickerDto dto);
        void Update(DeerLickerDto dto);
        void Delete(int id);
    }
}