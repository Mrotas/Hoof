using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.EmploymentPlan
{
    public class EmploymentPlanDao : DaoBase, IEmploymentPlanDao
    {
        public IList<EmploymentPlanDto> GetEmploymentPlan(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.EmploymentPlan> eployeePlan = db.EmploymentPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<EmploymentPlanDto> dtos = ToDtos(eployeePlan);

                return dtos;
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
