using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.FodderPlan
{
    public class FodderPlanDao : DaoBase, IFodderPlanDao
    {
        public IList<FodderPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.FodderPlan> fodderPlan = db.FodderPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                var dtos = ToDtos(fodderPlan);

                return dtos;
            }
        }

        private IList<FodderPlanDto> ToDtos(IList<Entities.FodderPlan> entityList)
        {
            var dtos = new List<FodderPlanDto>();
            foreach (Entities.FodderPlan entity in entityList)
            {
                var dto = new FodderPlanDto
                {
                    Id = entity.Id,
                    Type = entity.Type,
                    Ton = entity.Ton,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
