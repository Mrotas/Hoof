using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Pulpit
{
    public class PulpitDao : IPulpitDao
    {
        public IList<PulpitDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pulpit> pulpits = db.Pulpit.Where(x => !x.RemovedDate.HasValue && x.CreatedDate < marketingYear.End).ToList();

                IList<PulpitDto> dtos = ToDtos(pulpits);

                return dtos;
            }
        }

        private IList<PulpitDto> ToDtos(IList<Entities.Pulpit> entityList)
        {
            var dtos = new List<PulpitDto>();
            foreach (Entities.Pulpit entity in entityList)
            {
                var dto = new PulpitDto
                {
                    Id = entity.Id,
                    Section = entity.Section,
                    District = entity.District,
                    Forestry = entity.Forestry,
                    Description = entity.Description
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
