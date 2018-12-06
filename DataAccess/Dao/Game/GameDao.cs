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

        public IList<Entities.Game> Get(int type, int kind, int? subKind)
        {
            var games = new List<Entities.Game>();
            using (DbContext)
            {
                games = DbContext.Game.Where(x => x.Type == type && x.Kind == kind).ToList();

                if (subKind.HasValue)
                {
                    games = games.Where(x => x.SubKind == subKind.Value).ToList();
                }
            }

            return games;
        }
    }
}
