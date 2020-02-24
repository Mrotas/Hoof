using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;

namespace DataAccess.Dao.Region
{
    public class RegionDao : IRegionDao
    {
        public IList<RegionDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.Region> regions = db.Region.ToList();

                IList<RegionDto> dtos = ToDtos(regions);

                return dtos;
            }
        }

        public int GetRegionId(string city, int circuit, int district)
        {
            using (var db = new DbContext())
            {
                int regionId = db.Region.Where(x => x.City == city && x.Circuit == circuit && x.District == district).Select(x => x.Id).FirstOrDefault();
                return regionId;
            }
        }

        private IList<RegionDto> ToDtos(IList<Entities.Region> entityList)
        {
            var dtos = new List<RegionDto>();
            foreach (Entities.Region entity in entityList)
            {
                var dto = new RegionDto
                {
                    Id = entity.Id,
                    City = entity.City,
                    Circuit = entity.Circuit,
                    District = entity.District
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
