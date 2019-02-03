using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.GameHuntPlan
{
    public class GameHuntPlanDao : DaoBase, IGameHuntPlanDao
    {
        public IList<GameHuntPlanDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.GameHuntPlan> gameHuntPlan = db.GameHuntPlan.ToList();

                IList<GameHuntPlanDto> dtos = ToDtos(gameHuntPlan);

                return dtos;
            }
        }

        public IList<GameHuntPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.GameHuntPlan> gameHuntPlan = db.GameHuntPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<GameHuntPlanDto> dtos = ToDtos(gameHuntPlan);

                return dtos;
            }
        }
        
        private IList<GameHuntPlanDto> ToDtos(IList<Entities.GameHuntPlan> entityList)
        {
            var dtos = new List<GameHuntPlanDto>();
            foreach (Entities.GameHuntPlan entity in entityList)
            {
                var dto = new GameHuntPlanDto
                {
                    Id = entity.Id,
                    GameId = entity.GameId,
                    Class = entity.Class,
                    Catch = entity.Catch,
                    Cull = entity.Cull,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
