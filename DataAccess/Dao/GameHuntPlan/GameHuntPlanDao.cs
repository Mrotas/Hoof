using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.GameHuntPlan
{
    public class GameHuntPlanDao : IGameHuntPlanDao
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

        public void Insert(GameHuntPlanDto dto)
        {
            var entity = new Entities.GameHuntPlan
            {
                GameId = dto.GameId,
                Class = dto.Class,
                Cull = dto.Cull,
                Catch = dto.Catch,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.GameHuntPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(GameHuntPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.GameHuntPlan gameHuntPlan;
                if (dto.Id > 0)
                {
                    gameHuntPlan = db.GameHuntPlan.Single(x => x.Id == dto.Id);
                }
                else if (dto.GameId > 0 && dto.MarketingYearId > 0)
                {
                    gameHuntPlan = db.GameHuntPlan.Single(x => x.GameId == dto.GameId && x.MarketingYearId == dto.MarketingYearId);
                }
                else
                {
                    throw new Exception($"Nie można edytować planu pozyskania zwierzyny");
                }

                gameHuntPlan.Cull = dto.Cull;
                gameHuntPlan.Catch = dto.Catch;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.GameHuntPlan gameHuntPlan = db.GameHuntPlan.Find(id);

                db.GameHuntPlan.Remove(gameHuntPlan);
                db.SaveChanges();
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
