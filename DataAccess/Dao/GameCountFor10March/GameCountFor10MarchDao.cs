using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.GameCountFor10March
{
    public class GameCountFor10MarchDao : DaoBase, IGameCountFor10MarchDao
    {
        public IList<GameCountFor10MarchDto> GameCountBefore10MarchPlan(int marketingYearId)
        {
            using (DbContext)
            {
                List<Entities.GameCountFor10March> gameCountFor10MarchPlan = DbContext.GameCountFor10March.Where(x => x.MarketingYearId == marketingYearId).ToList();

                var dtos = ToDtos(gameCountFor10MarchPlan);

                return dtos;
            }
        }

        private IList<GameCountFor10MarchDto> ToDtos(IList<Entities.GameCountFor10March> entityList)
        {
            var dtos = new List<GameCountFor10MarchDto>();
            foreach (Entities.GameCountFor10March entity in entityList)
            {
                var dto = new GameCountFor10MarchDto
                {
                    Id = entity.Id,
                    GameId = entity.GameId,
                    Class = entity.Class,
                    Count = entity.Count,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
