using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;

namespace DataAccess.Dao.GameCountBeforeHuntingSeason
{
    public class GameCountBeforeHuntingSeasonPlanDao : DaoBase, IGameCountBeforeHuntingSeasonPlanDao
    {
        public List<GameCountBeforeHuntingSeasonPlan> GetGameCountBeforeHuntingSeasonPlan(int year)
        {
            var gameCountBeforeHuntingSeasonPlan = new List<GameCountBeforeHuntingSeasonPlan>();
            using (DbContext)
            {
                gameCountBeforeHuntingSeasonPlan = DbContext.GameCountBeforeHuntingSeason.Where(x => x.Year == year).ToList();
            }

            return gameCountBeforeHuntingSeasonPlan;
        }
    }
}
