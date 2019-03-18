using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.DeerLicker
{
    public class DeerLickerDao : IDeerLickerDao
    {
        public IList<DeerLickerDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.DeerLicker> deerLickers = db.DeerLicker.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<DeerLickerDto> dtos = ToDtos(deerLickers);

                return dtos;
            }
        }

        private IList<DeerLickerDto> ToDtos(IList<Entities.DeerLicker> entityList)
        {
            var dtos = new List<DeerLickerDto>();
            foreach (Entities.DeerLicker entity in entityList)
            {
                var dto = new DeerLickerDto
                {
                    Id = entity.Id,
                    Count = entity.Count,
                    Section = entity.Section,
                    District = entity.District,
                    Forestry = entity.Forestry,
                    Description = entity.Description,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
