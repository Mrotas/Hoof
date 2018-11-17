using System.Collections.Generic;

namespace DataAccess.Dao.Game
{
    public interface IGameDao
    {
        IList<Entities.Game> GetAll();
    }
}