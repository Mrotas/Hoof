using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.CostPlan
{
    public class CostPlanDao : ICostPlanDao
    {
        public IList<CostPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.CostPlan> costPlan = db.CostPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<CostPlanDto> costPlanDtos = ToDtos(costPlan);

                return costPlanDtos;
            }
        }

        public void Insert(CostPlanDto dto)
        {
            var entity = new Entities.CostPlan
            {
                Type = dto.Type,
                Cost = dto.Cost,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.CostPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(CostPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.CostPlan costPlan = db.CostPlan.Single(x => x.MarketingYearId == dto.MarketingYearId && x.Type == dto.Type);

                costPlan.Cost = dto.Cost;

                db.SaveChanges();
            }
        }

        public void Delete(int costType, int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.CostPlan costPlan = db.CostPlan.Single(x => x.MarketingYearId == marketingYearId && x.Type == costType);

                db.CostPlan.Remove(costPlan);
                db.SaveChanges();
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
