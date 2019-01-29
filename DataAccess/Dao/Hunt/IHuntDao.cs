using DataAccess.Dto;
using System;
using System.Collections.Generic;

namespace DataAccess.Dao.Hunt
{
    public interface IHuntDao
    {
        IList<HuntDto> GetAll();
        IList<HuntDto> GetHuntsByYear(int year);
        IList<HuntDto> GetHuntsByDateRange(DateTime startDate, DateTime endDate);
        void Insert(HuntDto huntDto);
    }
}