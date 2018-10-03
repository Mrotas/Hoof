using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Dao.Hunt
{
    public class HuntDao : DaoBase, IHuntDao
    {
        public IList<Entities.Hunt> GetAll()
        {
            var hunts = new List<Entities.Hunt>();
            using (DbContext)
            {
                hunts = DbContext.Hunt.ToList();
            }
            
            return hunts;
        }

        public void Insert(Entities.Hunt hunt)
        {
            using (DbContext)
            {
                DbContext.Hunt.Add(hunt);
                DbContext.SaveChanges();
            }
        }
    }
}
