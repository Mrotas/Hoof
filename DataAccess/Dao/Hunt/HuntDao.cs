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

        public IList<Entities.Hunt> GetHuntsByYear(int year)
        {
            var hunts = new List<Entities.Hunt>();
            using (DbContext)
            {
                hunts = DbContext.Hunt.Where(x => x.Date.Year == year).ToList();
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
