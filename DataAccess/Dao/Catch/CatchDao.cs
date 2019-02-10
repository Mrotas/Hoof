using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dto;

namespace DataAccess.Dao.Catch
{
    public class CatchDao : DaoBase, ICatchDao
    {
        public IList<CatchDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Catch> catches = DbContext.Catch.ToList();

                IList<CatchDto> dtos = ToDtos(catches);

                return dtos;
            }
        }

        public IList<CatchDto> GetByMarketingYear(int marketingYearId)
        {
            using (DbContext)
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);
                List<Entities.Catch> catches = DbContext.Catch.Where(x => x.Date > marketingYear.Start && x.Date < marketingYear.End).ToList();

                IList<CatchDto> dtos = ToDtos(catches);

                return dtos;
            }
        }

        public IList<CatchDto> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (DbContext)
            {
                List<Entities.Catch> catches = DbContext.Catch.Where(x => x.Date > fromDate && x.Date < toDate).ToList();

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

            using (DbContext)
            {
                DbContext.Catch.Add(gameCatch);
                DbContext.SaveChanges();
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
