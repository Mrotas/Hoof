using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.TrunkFoodPlan
{
    public class TrunkFoodPlanDao : DaoBase, ITrunkFoodPlanDao
    {
        public TrunkFoodPlanDto GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.TrunkFoodPlan trunkFoodPlan = db.TrunkFoodPlan.FirstOrDefault(x => x.MarketingYearId == marketingYearId);

                if (trunkFoodPlan == null)
                {
                    return null;
                }

                var dtos = ToDto(trunkFoodPlan);

                return dtos;
            }
        }

        public void Insert(TrunkFoodPlanDto dto)
        {
            var entity = new Entities.TrunkFoodPlan
            {
                Id = dto.Id,
                Hectare = dto.Hectare,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.TrunkFoodPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(TrunkFoodPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.TrunkFoodPlan trunkFoodPlan = db.TrunkFoodPlan.Single(x => x.MarketingYearId == dto.MarketingYearId);

                trunkFoodPlan.Hectare = dto.Hectare;

                db.SaveChanges();
            }
        }

        public void Delete(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.TrunkFoodPlan trunkFoodPlan = db.TrunkFoodPlan.Single(x => x.MarketingYearId == marketingYearId);

                db.TrunkFoodPlan.Remove(trunkFoodPlan);
                db.SaveChanges();
            }
        }

        private TrunkFoodPlanDto ToDto(Entities.TrunkFoodPlan entity)
        {
            var dto = new TrunkFoodPlanDto
            {
                Id = entity.Id,
                Hectare = entity.Hectare,
                MarketingYearId = entity.MarketingYearId
            };
            
            return dto;
        }
    }
}
