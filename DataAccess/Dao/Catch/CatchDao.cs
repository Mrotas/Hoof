using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Catch
{
    public class CatchDao : ICatchDao
    {
        public IList<CatchDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Catch> catches = db.Catch.ToList();

                IList<CatchDto> dtos = ToDtos(catches);

                return dtos;
            }
        }

        public IList<CatchDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Catch> catches = db.Catch.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<CatchDto> dtos = ToDtos(catches);

                return dtos;
            }
        }

        public IList<CatchDto> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (var db = new DbContext())
            {
                List<Entities.Catch> catches = db.Catch.Where(x => x.Date >= fromDate && x.Date <= toDate).ToList();

                var dtos = ToDtos(catches);

                return dtos;
            }
        }

        public void Insert(CatchDto catchDto)
        {
            var gameCatch = new Entities.Catch
            {
                HuntsmanId = catchDto.HuntsmanId,
                GameId = catchDto.GameId,
                RegionId = catchDto.RegionId,
                Date = catchDto.Date,
                Count = catchDto.Count
            };

            using (var db = new DbContext())
            {
                db.Catch.Add(gameCatch);
                db.SaveChanges();
            }
        }

        private IList<CatchDto> ToDtos(IList<Entities.Catch> entityList)
        {
            var dtos = new List<CatchDto>();
            foreach (Entities.Catch entity in entityList)
            {
                var dto = new CatchDto
                {
                    Id = entity.Id,
                    HuntsmanId = entity.HuntsmanId,
                    GameId = entity.GameId,
                    RegionId = entity.RegionId,
                    Date = entity.Date,
                    Count = entity.Count
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
