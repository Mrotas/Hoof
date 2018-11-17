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
    }
}
