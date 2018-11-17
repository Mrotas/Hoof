using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Huntsman
{
    public class HuntsmanDao : DaoBase, IHuntsmanDao
    {
        public IList<Entities.Huntsman> GetAll()
        {
            var huntsmans = new List<Entities.Huntsman>();
            using (DbContext)
            {
                huntsmans = DbContext.Huntsman.ToList();
            }

            return huntsmans;
        }
    }
}
