using System;
using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Labor
{
    public interface ILaborDao
    {
        IList<LaborDto> GetByMarketingYear(int marketingYearId);
        IList<LaborDto> GetByDateRange(DateTime fromDate, DateTime toDate);
        void Insert(LaborDto dto);
        void Update(LaborDto dto);
        void Delete(int id);
    }
}