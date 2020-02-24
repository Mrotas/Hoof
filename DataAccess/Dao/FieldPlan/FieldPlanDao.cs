using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.FieldPlan
{
    public class FieldPlanDao : IFieldPlanDao
    {
        public FieldPlanDto GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.FieldPlan fieldPlan = db.FieldPlan.FirstOrDefault(x => x.MarketingYearId == marketingYearId);

                if (fieldPlan == null)
                {
                    return null;
                }

                FieldPlanDto dto = ToDto(fieldPlan);

                return dto;
            }
        }

        public void Insert(FieldPlanDto dto)
        {
            var entity = new Entities.FieldPlan
            {
                Id = dto.Id,
                Hectare = dto.Hectare,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.FieldPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(FieldPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.FieldPlan fieldPlan = db.FieldPlan.Single(x => x.MarketingYearId == dto.MarketingYearId);

                fieldPlan.Hectare = dto.Hectare;

                db.SaveChanges();
            }
        }

        public void Delete(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.FieldPlan fieldPlan = db.FieldPlan.Single(x => x.MarketingYearId == marketingYearId);

                db.FieldPlan.Remove(fieldPlan);
                db.SaveChanges();
            }
        }

        private FieldPlanDto ToDto(Entities.FieldPlan entity)
        {
            var dto = new FieldPlanDto
            {
                Id = entity.Id,
                Hectare = entity.Hectare,
                MarketingYearId = entity.MarketingYearId
            };

            return dto;
        }
    }
}
