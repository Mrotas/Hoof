using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Region
{
    public class RegionDao : DaoBase, IRegionDao
    {
        public IList<Entities.Region> GetAll()
        {
            var regions = new List<Entities.Region>();
            using (DbContext)
            {
                regions = DbContext.Region.ToList();
            }

            return regions;
        }

        public int GetRegionId(string city, int circuit, int district)
        {
            using (DbContext)
            {
                List<Entities.Region> regions = DbContext.Region.ToList();
                int regionId = DbContext.Region
                    .Where(x => x.City == city &&
                                x.Circuit == circuit &&
                                x.District == district)
                    .Select(x => x.Id).FirstOrDefault();

                return regionId;
            }
        }
    }
}
