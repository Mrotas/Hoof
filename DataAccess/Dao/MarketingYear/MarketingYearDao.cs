using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dto;

namespace DataAccess.Dao.MarketingYear
{
    public class MarketingYearDao : DaoBase, IMarketingYearDao
    {
        public IList<MarketingYearDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.MarketingYear> marketingYears = DbContext.MarketingYear.ToList();

                IList<MarketingYearDto> dtos = ToDtos(marketingYears);

                return dtos;
            }
        }

        public int GetMarketingYearId(DateTime marketingYearStart, DateTime marketingYearEnd)
        {
            using (DbContext)
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.FirstOrDefault(x => x.Start.Equals(marketingYearStart) && x.End.Equals(marketingYearEnd));

                return marketingYear.Id;
            }
        }
        private IList<MarketingYearDto> ToDtos(IList<Entities.MarketingYear> entityList)
        {
            var dtos = new List<MarketingYearDto>();
            foreach (Entities.MarketingYear entity in entityList)
            {
                var dto = new MarketingYearDto
                {
                    Id = entity.Id,
                    Start = entity.Start,
                    End = entity.End
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
