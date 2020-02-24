using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.MarketingYear
{
    public class MarketingYearDao : IMarketingYearDao
    {
        public IList<MarketingYearDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.MarketingYear> marketingYears = db.MarketingYear.ToList();

                IList<MarketingYearDto> dtos = ToDtos(marketingYears);

                return dtos;
            }
        }

        public MarketingYearDto GetCurrent()
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.FirstOrDefault(x => x.Start <= DateTime.Now && DateTime.Now <= x.End);

                MarketingYearDto dto = marketingYear == null ? null : ToDto(marketingYear);

                return dto;
            }
        }

        public MarketingYearDto GetById(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);

                MarketingYearDto dto = marketingYear == null ? null : ToDto(marketingYear);

                return dto;
            }
        }
        
        public int Insert(MarketingYearDto dto)
        {
            var entity = new Entities.MarketingYear
            {
                Start = dto.Start,
                End = dto.End
            };

            using (var db = new DbContext())
            {
                Entities.MarketingYear newMarketingYear = db.MarketingYear.Add(entity);
                db.SaveChanges();
                return newMarketingYear.Id;
            }
        }

        private MarketingYearDto ToDto(Entities.MarketingYear entity)
        {
            var dto = new MarketingYearDto
            {
                Id = entity.Id,
                Start = entity.Start,
                End = entity.End
            };

            return dto;
        }

        private IList<MarketingYearDto> ToDtos(IList<Entities.MarketingYear> entityList)
        {
            var dtos = new List<MarketingYearDto>();
            foreach (Entities.MarketingYear entity in entityList)
            {
                MarketingYearDto dto = ToDto(entity);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
