using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.GameCountFor10March
{
    public interface IGameCountFor10MarchDao
    {
        IList<GameCountFor10MarchDto> GameCountBefore10MarchPlan(int marketingYearId);
    }
}