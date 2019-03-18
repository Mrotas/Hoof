using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Pulpit
{
    public class PulpitDao : IPulpitDao
    {
        public IList<PulpitDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pulpit> pulpits = db.Pulpit.Where(x => x.CreatedDate <= marketingYear.End && !x.RemovedDate.HasValue ||
                                                                        x.RemovedDate >= marketingYear.Start && x.RemovedDate <= marketingYear.End).ToList();

                IList<PulpitDto> dtos = ToDtos(pulpits);

                return dtos;
            }
        }

        public IList<PulpitDto> GetActiveByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pulpit> pulpits = db.Pulpit.Where(x => !x.RemovedDate.HasValue && x.CreatedDate <= marketingYear.End).ToList();

                IList<PulpitDto> dtos = ToDtos(pulpits);

                return dtos;
            }
        }

        public void Insert(PulpitDto dto)
        {
            var entity = new Entities.Pulpit
            {
                Number = dto.Number,
                Section = dto.Section,
                District = dto.District,
                Forestry = dto.Forestry,
                HasRoof = dto.HasRoof,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                RemovedDate = dto.RemovedDate
            };

            using (var db = new DbContext())
            {
                db.Pulpit.Add(entity);
                db.SaveChanges();
            }

        }

        public void Update(PulpitDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.Pulpit pulpit = db.Pulpit.Single(x => x.Id == dto.Id);

                pulpit.Number = dto.Number;
                pulpit.Section = dto.Section;
                pulpit.District = dto.District;
                pulpit.Forestry = dto.Forestry;
                pulpit.HasRoof = dto.HasRoof;
                pulpit.Description = dto.Description;
                pulpit.CreatedDate = dto.CreatedDate;
                pulpit.RemovedDate = dto.RemovedDate;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.Pulpit pulpit = db.Pulpit.Single(x => x.Id == id);

                db.Pulpit.Remove(pulpit);
                db.SaveChanges();
            }
        }

        private IList<PulpitDto> ToDtos(IList<Entities.Pulpit> entityList)
        {
            var dtos = new List<PulpitDto>();
            foreach (Entities.Pulpit entity in entityList)
            {
                var dto = new PulpitDto
                {
                    Id = entity.Id,
                    Number = entity.Number,
                    Section = entity.Section,
                    District = entity.District,
                    Forestry = entity.Forestry,
                    HasRoof = entity.HasRoof,
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
