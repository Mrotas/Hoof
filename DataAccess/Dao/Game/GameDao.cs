using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Game
{
    public class GameDao : DaoBase, IGameDao
    {
        public IList<GameDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Game> games = DbContext.Game.ToList();

                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }

        public IList<GameDto> GetByType(int gameType)
        {
            using (DbContext)
            {
                List<Entities.Game> games = DbContext.Game.Where(x => x.Type == gameType).ToList();

                IList<GameDto> dtos = ToDtos(games);

                return dtos;
            }
        }

        public IList<GameDto> Get(int type, int kind, int? subKind)
        {
            var games = new List<Entities.Game>();
            using (DbContext)
            {
                games = DbContext.Game.Where(x => x.Type == type && x.Kind == kind).ToList();

                if (subKind.HasValue)
                {
                    games = games.Where(x => x.SubKind == subKind.Value).ToList();
                }
            }

            IList<GameDto> dtos = ToDtos(games);

            return dtos;
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
