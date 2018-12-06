using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.AnnualPlan.GameHuntPlan
{
    public class GameHuntPlanDao : DaoBase, IGameHuntPlanDao
    {
        public List<Entities.AnnualPlan.GameHuntPlan> GetAll()
        {
            var gameHuntPlan = new List<Entities.AnnualPlan.GameHuntPlan>();
            using (var db = new DbContext())
            {
                gameHuntPlan = db.GamePlan.ToList();
            }

            return gameHuntPlan;
        }

        public List<Entities.AnnualPlan.GameHuntPlan> GetGameHuntPlan(int year)
        {
            var gameHuntPlan = new List<Entities.AnnualPlan.GameHuntPlan>();
            using (var db = new DbContext())
            {
                gameHuntPlan = db.GamePlan.Where(x => x.Year == year).ToList();
            }

            return gameHuntPlan;
        }
    }
}
