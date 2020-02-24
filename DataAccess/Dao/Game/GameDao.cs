using System;
using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.Game
{
    public class GameDao : IGameDao
    {
        public IList<GameDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Game> games = db.Game.ToList();

                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }

        public IList<GameDto> GetByType(int gameType)
        {
            using (var db = new DbContext())
            {
                List<Entities.Game> games = db.Game.Where(x => x.Type == gameType).ToList();

                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }

        public IList<GameDto> GetByKindName(string kindName)
        {
            using (var db = new DbContext())
            {
                List<Entities.Game> games = db.Game.Where(x => x.KindName == kindName).ToList();
                
                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }
        
        public IList<GameDto> Get(int type, int kind, int? subKind)
        {
            using (var db = new DbContext())
            {
                List<Entities.Game> games = db.Game.Where(x => x.Type == type && x.Kind == kind).ToList();

                if (subKind.HasValue)
                {
                    games = games.Where(x => x.SubKind == subKind.Value).ToList();
                }

                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }
        
        private IList<GameDto> ToDtos(IList<Entities.Game> entityList)
        {
            var dtos = new List<GameDto>();
            foreach (Entities.Game entity in entityList)
            {
                var dto = new GameDto
                {
                    Id = entity.Id,
                    Type = entity.Type,
                    Kind = entity.Kind,
                    KindName = entity.KindName,
                    SubKind = entity.SubKind,
                    SubKindName = entity.SubKindName
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
