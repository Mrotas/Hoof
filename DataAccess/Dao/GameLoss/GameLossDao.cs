using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.GameLoss
{
    public class GameLossDao : IGameLossDao
    {
        public IList<GameLossDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.GameLoss> lossGames = db.GameLoss.ToList();

                IList<GameLossDto> dtos = ToDtos(lossGames);

                return dtos;
            }
        }

        public void Insert(GameLossDto gameLossDto)
        {
            var gameLoss = new Entities.GameLoss
            {
                GameId = gameLossDto.GameId,
                SanitaryLoss = gameLossDto.SanitaryLoss,
                RegionId = gameLossDto.RegionId,
                Date = gameLossDto.Date,
                Description = gameLossDto.Description
            };

            using (var db = new DbContext())
            {
                db.GameLoss.Add(gameLoss);
                db.SaveChanges();
            }
        }

        private IList<GameLossDto> ToDtos(IList<Entities.GameLoss> entityList)
        {
            var dtos = new List<GameLossDto>();
            foreach (Entities.GameLoss entity in entityList)
            {
                var dto = new GameLossDto
                {
                    Id = entity.Id,
                    GameId = entity.GameId,
                    SanitaryLoss = entity.SanitaryLoss,
                    RegionId = entity.RegionId,
                    Date = entity.Date,
                    Description = entity.Description
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
