using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.EmploymentPlan
{
    public class EmploymentPlanDao : IEmploymentPlanDao
    {
        public IList<EmploymentPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.EmploymentPlan> eployeePlan = db.EmploymentPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<EmploymentPlanDto> dtos = ToDtos(eployeePlan);

                return dtos;
            }
        }

        public void Insert(EmploymentPlanDto dto)
        {
            var entity = new Entities.EmploymentPlan
            {
                EmploymentType = dto.EmploymentType,
                Count = dto.Count,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.EmploymentPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(EmploymentPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.EmploymentPlan employmentPlan = db.EmploymentPlan.Single(x => x.MarketingYearId == dto.MarketingYearId && x.EmploymentType == dto.EmploymentType);

                employmentPlan.Count = dto.Count;

                db.SaveChanges();
            }
        }

        public void Delete(int employmentType, int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.EmploymentPlan employmentPlan = db.EmploymentPlan.Single(x => x.MarketingYearId == marketingYearId && x.EmploymentType == employmentType);

                db.EmploymentPlan.Remove(employmentPlan);
                db.SaveChanges();
            }
        }

        private IList<EmploymentPlanDto> ToDtos(IList<Entities.EmploymentPlan> entityList)
        {
            var dtos = new List<EmploymentPlanDto>();
            foreach (Entities.EmploymentPlan entity in entityList)
            {
                var dto = new EmploymentPlanDto
                {
                    Id = entity.Id,
                    Count = entity.Count,
                    EmploymentType = entity.EmploymentType,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
