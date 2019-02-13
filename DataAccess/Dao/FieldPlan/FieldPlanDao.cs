using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.FieldPlan
{
    public class FieldPlanDao : DaoBase, IFieldPlanDao
    {
        public IList<FieldPlanDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.FieldPlan> fieldPlan = db.FieldPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<FieldPlanDto> dtos = ToDtos(fieldPlan);

                return dtos;
            }           
        }

        private IList<FieldPlanDto> ToDtos(IList<Entities.FieldPlan> entityList)
        {
            var dtos = new List<FieldPlanDto>();
            foreach (Entities.FieldPlan entity in entityList)
            {
                var dto = new FieldPlanDto
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
