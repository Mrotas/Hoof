using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Hunt
{
    public class HuntDao : DaoBase, IHuntDao
    {
        public IList<HuntDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Hunt> hunts = DbContext.Hunt.ToList();

                IList<HuntDto> dtos = ToDtos(hunts);

                return dtos;
            }
        }

        public IList<HuntDto> GetByMarketingYear(int marketingYearId)
        {
            using (DbContext)
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);
                List<Entities.Hunt> hunts = DbContext.Hunt.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<HuntDto> dtos = ToDtos(hunts);

                return dtos;
            }
        }

        public IList<HuntDto> GetHuntsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (DbContext)
            {
                List<Entities.Hunt> hunts = DbContext.Hunt.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

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

            using (DbContext)
            {
                DbContext.Hunt.Add(hunt);
                DbContext.SaveChanges();
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
