using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.GameCountBefore10MarchPlan
{
    public interface IGameCountBefore10MarchPlanDao
    {
        List<GameCountBefore10March> GameCountBefore10MarchPlan(int year);
    }
}