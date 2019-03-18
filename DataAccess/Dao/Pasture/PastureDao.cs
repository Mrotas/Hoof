using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Pasture
{
    public class PastureDao : DaoBase, IPastureDao
    {
        public IList<PastureDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pasture> pastures = db.Pasture.Where(x => x.CreatedDate <= marketingYear.End && !x.RemovedDate.HasValue || 
                                                                        x.RemovedDate >= marketingYear.Start && x.RemovedDate <= marketingYear.End).ToList();

                IList<PastureDto> dtos = ToDtos(pastures);

                return dtos;
            }
        }

        public IList<PastureDto> GetActiveByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pasture> pastures = db.Pasture.Where(x => !x.RemovedDate.HasValue && x.CreatedDate <= marketingYear.End).ToList();

                IList<PastureDto> dtos = ToDtos(pastures);

                return dtos;
            }
        }

        public void Insert(PastureDto dto)
        {
            var entity = new Entities.Pasture
            {
                Section = dto.Section,
                District = dto.District,
                Forestry = dto.Forestry,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                RemovedDate = dto.RemovedDate
            };

            using (var db = new DbContext())
            {
                db.Pasture.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(PastureDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.Pasture pasture = db.Pasture.Single(x => x.Id == dto.Id);

                pasture.Section = dto.Section;
                pasture.District = dto.District;
                pasture.Forestry = dto.Forestry;
                pasture.Description = dto.Description;
                pasture.CreatedDate = dto.CreatedDate;
                pasture.RemovedDate = dto.RemovedDate;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.Pasture pasture = db.Pasture.Single(x => x.Id == id);

                db.Pasture.Remove(pasture);
                db.SaveChanges();
            }
        }

        private IList<PastureDto> ToDtos(IList<Entities.Pasture> entityList)
        {
            var dtos = new List<PastureDto>();
            foreach (Entities.Pasture entity in entityList)
            {
                var dto = new PastureDto
                {
                    Id = entity.Id,
                    Section = entity.Section,
                    District = entity.District,
                    Forestry = entity.Forestry,
                    Description = entity.Description,
                    CreatedDate = entity.CreatedDate,
                    RemovedDate = entity.RemovedDate
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
