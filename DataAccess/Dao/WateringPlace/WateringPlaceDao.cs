using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.WateringPlace
{
    public class WateringPlaceDao : IWateringPlaceDao
    {
        public IList<WateringPlaceDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = db.MarketingYear.Find(marketingYearId);
                List<Entities.WateringPlace> wateringPlaces = db.WateringPlace.Where(x => x.CreatedDate <= marketingYear.End && !x.RemovedDate.HasValue ||
                                                                        x.RemovedDate >= marketingYear.Start && x.RemovedDate <= marketingYear.End).ToList();

                IList<WateringPlaceDto> dtos = ToDtos(wateringPlaces);

                return dtos;
            }
        }

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

        public void Insert(WateringPlaceDto dto)
        {
            var entity = new Entities.WateringPlace
            {
                Section = dto.Section,
                District = dto.District,
                Forestry = dto.Forestry,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate,
                RemovedDate = dto.RemovedDate
            };

            using (var db = new DbContext())
            {
                db.WateringPlace.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(WateringPlaceDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.WateringPlace wateringPlace = db.WateringPlace.Single(x => x.Id == dto.Id);

                wateringPlace.Section = dto.Section;
                wateringPlace.District = dto.District;
                wateringPlace.Forestry = dto.Forestry;
                wateringPlace.Description = dto.Description;
                wateringPlace.CreatedDate = dto.CreatedDate;
                wateringPlace.RemovedDate = dto.RemovedDate;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.WateringPlace wateringPlace = db.WateringPlace.Single(x => x.Id == id);

                db.WateringPlace.Remove(wateringPlace);
                db.SaveChanges();
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
