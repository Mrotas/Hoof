using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.LossGame
{
    public interface ILossGameDao
    {
        IList<LossGameDto> GetAll();
        IList<LossGameDto> GetByMarketingYear(int marketingYearId);
        IList<LossGameDto> GetSanitaryLossesByMarketingYear(int marketingYearId);
    }
}