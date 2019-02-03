using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Dto;

namespace DataAccess.Dao.Loss
{
    public class LossDao : DaoBase, ILossDao
    {
        public IList<LossDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Loss> losses = DbContext.Loss.ToList();

                IList<LossDto> dtos = ToDtos(losses);

                return dtos;
            }
        }

        public IList<LossDto> GetByMarketingYear(int marketingYearId)
        {
            using (DbContext)
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);

                List<Entities.Loss> losses = DbContext.Loss.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<LossDto> dtos = ToDtos(losses);

                return dtos;
            }
        }

        public void Insert(LossGameDto lossGameDto, LossDto lossDto)
        {
            using (DbContext)
            {
                using (DbContextTransaction transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var lossGameEntity = new Entities.LossGame
                        {
                            GameId = lossGameDto.GameId,
                            Class = lossGameDto.Class
                        };

                        DbContext.LossGame.Add(lossGameEntity);
                        DbContext.SaveChanges();

                        var lossEntity = new Entities.Loss
                        {
                            LossGameId = lossGameEntity.Id,
                            RegionId = lossDto.RegionId,
                            Date = lossDto.Date,
                            Description = lossDto.Description,
                            SanitaryLoss = lossDto.SanitaryLoss
                        };

                        DbContext.Loss.Add(lossEntity);
                        DbContext.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        private IList<LossDto> ToDtos(IList<Entities.Loss> entityList)
        {
            var dtos = new List<LossDto>();
            foreach (Entities.Loss entity in entityList)
            {
                var dto = new LossDto
                {
                    Id = entity.Id,
                    LossGameId = entity.LossGameId,
                    RegionId = entity.RegionId,
                    Date = entity.Date,
                    Description = entity.Description,
                    SanitaryLoss = entity.SanitaryLoss
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
