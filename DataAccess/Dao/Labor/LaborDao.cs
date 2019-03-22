using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Labor
{
    public class LaborDao: DaoBase, ILaborDao
    {
        public IList<LaborDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);
                List<Entities.Labor> expenses = db.Labor.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<LaborDto> dtos = ToDtos(expenses);

                return dtos;
            }
        }

        public void Insert(LaborDto dto)
        {
            var entity = new Entities.Labor
            {
                HuntsmanId = dto.HuntsmanId,
                Description = dto.Description,
                Date = dto.Date
            };

            using (var db = new DbContext())
            {
                db.Labor.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(LaborDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.Labor labor = db.Labor.Single(x => x.Id == dto.Id);

                labor.HuntsmanId = dto.HuntsmanId;
                labor.Description = dto.Description;
                labor.Date = dto.Date;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.Labor labor = db.Labor.Single(x => x.Id == id);

                db.Labor.Remove(labor);
                db.SaveChanges();
            }
        }

        private IList<LaborDto> ToDtos(IList<Entities.Labor> entityList)
        {
            var dtos = new List<LaborDto>();
            foreach (Entities.Labor entity in entityList)
            {
                var dto = new LaborDto
                {
                    Id = entity.Id,
                    HuntsmanId = entity.HuntsmanId,
                    Description = entity.Description,
                    Date = entity.Date
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
