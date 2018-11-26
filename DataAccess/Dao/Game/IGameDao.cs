using System.Collections.Generic;

namespace DataAccess.Dao.Game
{
    public interface IGameDao
    {
        IList<Entities.Game> GetAll();
        IList<Entities.Game> Get(int type, int kind, int? subKind);
    }
}