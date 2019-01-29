using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.TrunkFoodPlan
{
    public class TrunkFoodPlanDao : DaoBase, ITrunkFoodPlanDao
    {
        public IList<TrunkFoodPlanDto> GetTrunkFoodPlan(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.TrunkFoodPlan> trunkFoodPlan = db.TrunkFoodPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                var dtos = ToDtos(trunkFoodPlan);

                return dtos;
            }
        }

        private IList<TrunkFoodPlanDto> ToDtos(IList<Entities.TrunkFoodPlan> entityList)
        {
            var dtos = new List<TrunkFoodPlanDto>();
            foreach (Entities.TrunkFoodPlan entity in entityList)
            {
                var dto = new TrunkFoodPlanDto
                {
                    Id = entity.Id,
                    Hectare = entity.Hectare,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
