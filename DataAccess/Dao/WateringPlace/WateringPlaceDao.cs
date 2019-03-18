using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.WateringPlace
{
    public class WateringPlaceDao : IWateringPlaceDao
    {
        public IList<WateringPlaceDto> GetActiveByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.WateringPlace> wateringPlaces = db.WateringPlace.Where(x => !x.RemovedDate.HasValue && x.CreatedDate <= marketingYear.End).ToList();

                IList<WateringPlaceDto> dtos = ToDtos(wateringPlaces);

                return dtos;
            }
        }

        private IList<WateringPlaceDto> ToDtos(IList<Entities.WateringPlace> entityList)
        {
            var dtos = new List<WateringPlaceDto>();
            foreach (Entities.WateringPlace entity in entityList)
            {
                var dto = new WateringPlaceDto
                {
                    Id = entity.Id,
                    Section = entity.Section,
                    District = entity.District,
                    Forestry = entity.Forestry,
                    Description = entity.Description,
                    CreatedDate = entity.CreatedDate,
                    RemovedDate = entity.RemovedDate
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
