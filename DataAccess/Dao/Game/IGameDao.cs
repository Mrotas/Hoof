using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.Game
{
    public interface IGameDao
    {
        IList<GameDto> GetAll();
        IList<GameDto> GetByType(int gameType);
        IList<GameDto> Get(int type, int kind, int? subKind);
    }
}