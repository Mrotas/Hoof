using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.Game
{
    public class GamePlanDao : DaoBase, IGamePlanDao
    {
        public List<GamePlan> GetGamePlan(int year)
        {
            var gamePlan = new List<GamePlan>();
            using (var db = new DbContext())
            {
                gamePlan = db.GamePlan.Where(x => x.Year == year).ToList();
            }

            return gamePlan;
        }
    }
}
