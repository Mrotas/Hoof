using System.Collections.Generic;

namespace DataAccess.Dao.Hunt
{
    public interface IHuntDao
    {
        IList<Entities.Hunt> GetAll();
        IList<Entities.Hunt> GetHuntsByYear(int year);
        void Insert(Entities.Hunt hunt);
    }
}