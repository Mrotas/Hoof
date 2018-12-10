using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.GameLoss
{
    public class GameLossDao : IGameLossDao
    {
        public List<Entities.GameLoss> GetAll()
        {
            var lossGames = new List<Entities.GameLoss>();
            using (var db = new DbContext())
            {
                lossGames = db.GameLoss.ToList();
            }

            return lossGames;
        }

        public void Insert(Entities.GameLoss gameLoss)
        {
            using (var db = new DbContext())
            {
                db.GameLoss.Add(gameLoss);
                db.SaveChanges();
            }
        }
    }
}
