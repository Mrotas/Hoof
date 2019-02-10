using System;
using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Catch
{
    public interface ICatchDao
    {
        IList<CatchDto> GetAll();
        IList<CatchDto> GetByMarketingYear(int marketingYearId);
        IList<CatchDto> GetByDateRange(DateTime fromDate, DateTime toDate);
        void Insert(CatchDto catchDto);
    }
}