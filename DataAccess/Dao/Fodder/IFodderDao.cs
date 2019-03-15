using System;
using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Fodder
{
    public interface IFodderDao
    {
        IList<FodderDto> GetByMarketingYear(int marketingYearId);
        IList<FodderDto> GetByDateRange(DateTime dateFrom, DateTime dateTo);
        void Insert(FodderDto dto);
        void Update(FodderDto dto);
        void Delete(int id);
    }
}