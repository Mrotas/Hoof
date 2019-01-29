using DataAccess.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Region
{
    public class RegionDao : DaoBase, IRegionDao
    {
        public IList<RegionDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.Region> regions = DbContext.Region.ToList();

                IList<RegionDto> dtos = ToDtos(regions);

                return dtos;
            }
        }

        public int GetRegionId(string city, int circuit, int district)
        {
            using (DbContext)
            {
                int regionId = DbContext.Region.Where(x => x.City == city && x.Circuit == circuit && x.District == district).Select(x => x.Id).FirstOrDefault();
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
