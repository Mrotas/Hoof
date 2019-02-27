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

        public void Insert(FodderPlanDto dto)
        {
            var entity = new Entities.FodderPlan
            {
                Type = dto.Type,
                Ton = dto.Ton,
                MarketingYearId = dto.MarketingYearId
            };

            using (var db = new DbContext())
            {
                db.FodderPlan.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(FodderPlanDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.FodderPlan fodderPlan = db.FodderPlan.Single(x => x.MarketingYearId == dto.MarketingYearId && x.Type == dto.Type);

                fodderPlan.Ton = dto.Ton;

                db.SaveChanges();
            }
        }

        public void Delete(int fodderType, int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.FodderPlan fodderPlan = db.FodderPlan.Single(x => x.MarketingYearId == marketingYearId && x.Type == fodderType);

                db.FodderPlan.Remove(fodderPlan);
                db.SaveChanges();
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
