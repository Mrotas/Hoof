using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Dto;
using DbContext = DataAccess.Context.DbContext;

namespace DataAccess.Dao.Loss
{
    public class LossDao : ILossDao
    {
        public IList<LossDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Loss> losses = db.Loss.ToList();

                IList<LossDto> dtos = ToDtos(losses);

                return dtos;
            }
        }

        public IList<LossDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);

                List<Entities.Loss> losses = db.Loss.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<LossDto> dtos = ToDtos(losses);

                return dtos;
            }
        }

        public void Insert(LossGameDto lossGameDto, LossDto lossDto)
        {
            using (var db = new DbContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var lossGameEntity = new Entities.LossGame
                        {
                            GameId = lossGameDto.GameId,
                            Class = lossGameDto.Class
                        };

                        db.LossGame.Add(lossGameEntity);
                        db.SaveChanges();

                        var lossEntity = new Entities.Loss
                        {
                            LossGameId = lossGameEntity.Id,
                            RegionId = lossDto.RegionId,
                            Date = lossDto.Date,
                            Description = lossDto.Description,
                            SanitaryLoss = lossDto.SanitaryLoss
                        };

                        db.Loss.Add(lossEntity);
                        db.SaveChanges();

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
