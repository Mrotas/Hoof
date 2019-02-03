using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.HuntedGame
{
    public interface IHuntedGameDao
    {
        IList<HuntedGameDto> GetAll();
        int Insert(HuntedGameDto huntedGameDto);
        IList<HuntedGameDto> GetByMarketingYear(int marketingYearId);
    }
}