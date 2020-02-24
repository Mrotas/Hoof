using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.HuntedGame
{
    public class HuntedGameDao : IHuntedGameDao
    {
        public IList<HuntedGameDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.HuntedGame> huntedGames = db.HuntedGame.ToList();

                IList<HuntedGameDto> dtos = ToDtos(huntedGames);

                return dtos;
            }
        }
        
        public IList<HuntedGameDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<int?> huntedGameIds = db.Hunt.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End && x.HuntedGameId != null).Select(x => x.HuntedGameId).ToList();

                List<Entities.HuntedGame> huntedGames = db.HuntedGame.Where(x => huntedGameIds.Contains(x.Id)).ToList();

                IList<HuntedGameDto> dtos = ToDtos(huntedGames);

                return dtos;
            }
        }

        public IList<HuntedGameDto> GetByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            using (var db = new DbContext())
            {
                List<int?> huntedGameIds = db.Hunt.Where(x => x.Date >= dateFrom && x.Date <= dateTo && x.HuntedGameId != null).Select(x => x.HuntedGameId).ToList();

                List<Entities.HuntedGame> huntedGames = db.HuntedGame.Where(x => huntedGameIds.Contains(x.Id)).ToList();

                IList<HuntedGameDto> dtos = ToDtos(huntedGames);

                return dtos;
            }
        }

        public int Insert(HuntedGameDto huntedGameDto)
        {
            using (var db = new DbContext())
            {
                Entities.HuntedGame huntedGame = ToEntity(huntedGameDto);
                Entities.HuntedGame addedHuntedGame = db.HuntedGame.Add(huntedGame);

                db.SaveChanges();

                return addedHuntedGame.Id;
            }
        }

        private Entities.HuntedGame ToEntity(HuntedGameDto huntedGameDto)
        {
            return new Entities.HuntedGame
            {
                GameId = huntedGameDto.GameId,
                GameClass = huntedGameDto.GameClass,
                GameWeight = huntedGameDto.GameWeight
            };
        }

        private IList<HuntedGameDto> ToDtos(IList<Entities.HuntedGame> entityList)
        {
            var dtos = new List<HuntedGameDto>();
            foreach (Entities.HuntedGame entity in entityList)
            {
                var dto = new HuntedGameDto
                {
                    Id = entity.Id,
                    GameId = entity.GameId,
                    GameClass = entity.GameClass,
                    GameWeight = entity.GameWeight
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
