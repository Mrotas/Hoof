using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.HuntEquipmentPlan
{
    public class HuntEquipmentPlanDao : DaoBase, IHuntEquipmentPlanDao
    {
        public IList<HuntEquipmentPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.HuntEquipmentPlan> huntEquipmentPlan = db.HuntEquipmentPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<HuntEquipmentPlanDto> dtos = ToDtos(huntEquipmentPlan);

                return dtos;
            }
        }

        public void Insert(HuntEquipmentPlanDto dto)
        {
            var huntEquipmentPlan = new Entities.HuntEquipmentPlan
            {
                Type = dto.Type,
                Count = dto.Count,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.HuntEquipmentPlan.Add(huntEquipmentPlan);
                db.SaveChanges();
            }
        }

        public void Update(HuntEquipmentPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.HuntEquipmentPlan huntEquipmentPlan = db.HuntEquipmentPlan.Single(x => x.MarketingYearId == dto.MarketingYearId && x.Type == dto.Type);

                huntEquipmentPlan.Count = dto.Count;

                db.SaveChanges();
            }
        }

        private IList<HuntEquipmentPlanDto> ToDtos(IList<Entities.HuntEquipmentPlan> entityList)
        {
            var dtos = new List<HuntEquipmentPlanDto>();
            foreach (Entities.HuntEquipmentPlan entity in entityList)
            {
                var dto = new HuntEquipmentPlanDto
                {
                    Id = entity.Id,
                    Type = entity.Type,
                    Count = entity.Count,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
