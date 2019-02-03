using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.LossGame
{
    public class LossGameDao : DaoBase, ILossGameDao
    {
        public IList<LossGameDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.LossGame> lossGames = DbContext.LossGame.ToList();

                IList<LossGameDto> dtos = ToDtos(lossGames);

                return dtos;
            }
        }

        public IList<LossGameDto> GetByMarketingYear(int marketingYearId)
        {
            using (DbContext)
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);

                List<Entities.LossGame> lossGames = (from loss in DbContext.Loss
                                                    join lossGame in DbContext.LossGame on loss.LossGameId equals lossGame.Id
                                                    where loss.Date >= marketingYear.Start && loss.Date <= marketingYear.End
                                                    select lossGame).ToList();

                IList<LossGameDto> dtos = ToDtos(lossGames);

                return dtos;
            }
        }

        public IList<LossGameDto> GetSanitaryLossesByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);

                List<Entities.LossGame> lossGames = (from loss in db.Loss
                                                    join lossGame in db.LossGame on loss.LossGameId equals lossGame.Id
                                                    where loss.Date >= marketingYear.Start && loss.Date <= marketingYear.End && loss.SanitaryLoss
                                                    select lossGame).ToList();

                IList<LossGameDto> dtos = ToDtos(lossGames);

                return dtos;
            }
        }
        public int Insert(LossGameDto dto)
        {
            var entity = new Entities.LossGame
            {
                GameId = dto.GameId,
                Class = dto.Class
            };

            using (DbContext)
            {
                Entities.LossGame addedLossGame = DbContext.LossGame.Add(entity);
                DbContext.SaveChanges();
                return addedLossGame.Id;
            }
        }
        private IList<LossGameDto> ToDtos(IList<Entities.LossGame> entityList)
        {
            var dtos = new List<LossGameDto>();
            foreach (Entities.LossGame entity in entityList)
            {
                var dto = new LossGameDto
                {
                    Id = entity.Id,
                    GameId = entity.GameId,
                    Class = entity.Class
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
