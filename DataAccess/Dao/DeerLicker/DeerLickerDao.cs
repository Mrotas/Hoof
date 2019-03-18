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

        public void Insert(DeerLickerDto dto)
        {
            var entity = new Entities.DeerLicker
            {
                Count = dto.Count,
                Section = dto.Section,
                District = dto.District,
                Forestry = dto.Forestry,
                Description = dto.Description,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.DeerLicker.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(DeerLickerDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.DeerLicker deerLicker = db.DeerLicker.Single(x => x.Id == dto.Id);

                deerLicker.Count = dto.Count;
                deerLicker.Section = dto.Section;
                deerLicker.District = dto.District;
                deerLicker.Forestry = dto.Forestry;
                deerLicker.Description = dto.Description;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.DeerLicker deerLicker = db.DeerLicker.Single(x => x.Id == id);

                db.DeerLicker.Remove(deerLicker);
                db.SaveChanges();
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
