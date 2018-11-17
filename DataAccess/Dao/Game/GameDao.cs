using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Game
{
    public class GameDao : DaoBase, IGameDao
    {
        public IList<Entities.Game> GetAll()
        {
            var games = new List<Entities.Game>();
            using (DbContext)
            {
                games = DbContext.Game.ToList();
            }

            return games;
        }
    }
}
