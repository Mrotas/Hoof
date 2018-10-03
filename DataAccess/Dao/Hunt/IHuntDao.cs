using System.Collections.Generic;

namespace DataAccess.Dao.Hunt
{
    public interface IHuntDao
    {
        IList<Entities.Hunt> GetAll();
        void Insert(Entities.Hunt hunt);
    }
}