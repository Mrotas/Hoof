using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.CostPlan
{
    public class CostPlanDao : DaoBase, ICostPlanDao
    {
        public IList<CostPlanDto> GetCostPlan(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.CostPlan> costPlan = db.CostPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<CostPlanDto> costPlanDtos = ToDtos(costPlan);

                return costPlanDtos;
            }
        }

        private IList<CostPlanDto> ToDtos(IList<Entities.CostPlan> costPlanList)
        {
            var costPlanDtos = new List<CostPlanDto>();
            foreach (Entities.CostPlan costPlan in costPlanList)
            {
                var costPlanDto = new CostPlanDto
                {
                    Id = costPlan.Id,
                    Type = costPlan.Type,
                    Cost = costPlan.Cost,
                    MarketingYearId = costPlan.MarketingYearId
                };
                costPlanDtos.Add(costPlanDto);
            }
            return costPlanDtos;
        }
    }
}
