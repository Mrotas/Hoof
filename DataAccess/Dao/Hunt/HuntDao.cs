using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.Hunt
{
    public class HuntDao : IHuntDao
    {
        public IList<HuntDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Hunt> hunts = db.Hunt.ToList();

                IList<HuntDto> dtos = ToDtos(hunts);

                return dtos;
            }
        }

        public IList<HuntDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Hunt> hunts = db.Hunt.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<HuntDto> dtos = ToDtos(hunts);

                return dtos;
            }
        }

        public IList<HuntDto> GetHuntsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var db = new DbContext())
            {
                List<Entities.Hunt> hunts = db.Hunt.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

                var dtos = ToDtos(hunts);

                return dtos;
            }
        }

        public void Insert(HuntDto huntDto)
        {
            var hunt = new Entities.Hunt
            {
                HuntsmanId = huntDto.HuntsmanId,
                HuntedGameId = huntDto.HuntedGameId,
                RegionId = huntDto.RegionId,
                Date = huntDto.Date,
                Shots = huntDto.Shots
            };

            using (var db = new DbContext())
            {
                db.Hunt.Add(hunt);
                db.SaveChanges();
            }
        }

        private IList<HuntDto> ToDtos(IList<Entities.Hunt> entityList)
        {
            var dtos = new List<HuntDto>();
            foreach (Entities.Hunt entity in entityList)
            {
                var dto = new HuntDto
                {
                    Id = entity.Id,
                    HuntsmanId = entity.HuntsmanId,
                    HuntedGameId = entity.HuntedGameId,
                    RegionId = entity.RegionId,
                    Date = entity.Date,
                    Shots = entity.Shots
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
