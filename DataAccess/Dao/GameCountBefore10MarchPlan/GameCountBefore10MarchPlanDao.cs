using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;

namespace DataAccess.Dao.GameCountBefore10MarchPlan
{
    public class GameCountBefore10MarchPlanDao : DaoBase, IGameCountBefore10MarchPlanDao
    {
        public List<GameCountBefore10March> GameCountBefore10MarchPlan(int year)
        {
            var gameCountBefore10MarchPlan = new List<GameCountBefore10March>();
            using (DbContext)
            {
                gameCountBefore10MarchPlan = DbContext.GameCountBefore10MarchPlan.Where(x => x.Year == year).ToList();
            }

            return gameCountBefore10MarchPlan;
        }
    }
}
