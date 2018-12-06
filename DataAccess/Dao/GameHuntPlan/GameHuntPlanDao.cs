using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.GameHuntPlan
{
    public class GameHuntPlanDao : DaoBase, IGameHuntPlanDao
    {
        public List<Entities.GameHuntPlan> GetAll()
        {
            var gameHuntPlan = new List<Entities.GameHuntPlan>();
            using (var db = new DbContext())
            {
                gameHuntPlan = db.GameHuntPlan.ToList();
            }

            return gameHuntPlan;
        }

        public List<Entities.GameHuntPlan> GetGameHuntPlan(int year)
        {
            var gameHuntPlan = new List<Entities.GameHuntPlan>();
            using (var db = new DbContext())
            {
                gameHuntPlan = db.GameHuntPlan.Where(x => x.Year == year).ToList();
            }

            return gameHuntPlan;
        }
    }
}
