using System;
using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.GameCountFor10March
{
    public class GameCountFor10MarchDao : IGameCountFor10MarchDao
    {
        public IList<GameCountFor10MarchDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.GameCountFor10March> gameCountFor10MarchPlan = db.GameCountFor10March.Where(x => x.MarketingYearId == marketingYearId).ToList();

                var dtos = ToDtos(gameCountFor10MarchPlan);

                return dtos;
            }
        }

        public void Insert(GameCountFor10MarchDto dto)
        {
            var entity = new Entities.GameCountFor10March
            {
                GameId = dto.GameId,
                Class = dto.Class,
                Count = dto.Count,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.GameCountFor10March.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(GameCountFor10MarchDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.GameCountFor10March gameCountFor10MarchPlan;
                if (dto.Id > 0)
                {
                    gameCountFor10MarchPlan = db.GameCountFor10March.Single(x => x.Id == dto.Id);
                }
                else if (dto.GameId > 0 && dto.MarketingYearId > 0)
                {
                    gameCountFor10MarchPlan = db.GameCountFor10March.Single(x => x.GameId == dto.GameId && x.MarketingYearId == dto.MarketingYearId);
                }
                else
                {
                    throw new Exception($"Nie można edytować planu liczebności zwierzyny");
                }

                gameCountFor10MarchPlan.Count = dto.Count;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.GameCountFor10March gameCountFor10March = db.GameCountFor10March.Find(id);

                db.GameCountFor10March.Remove(gameCountFor10March);
                db.SaveChanges();
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
