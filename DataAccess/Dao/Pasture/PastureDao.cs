using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Pasture
{
    public class PastureDao : DaoBase, IPastureDao
    {
        public IList<PastureDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.Pasture> pastures = db.Pasture.Where(x => !x.RemovedDate.HasValue && x.CreatedDate < marketingYear.End).ToList();

                IList<PastureDto> dtos = ToDtos(pastures);

                return dtos;
            }
        }

        private IList<PastureDto> ToDtos(IList<Entities.Pasture> entityList)
        {
            var dtos = new List<PastureDto>();
            foreach (Entities.Pasture entity in entityList)
            {
                var dto = new PastureDto
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
