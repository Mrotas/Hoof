using System.Collections.Generic;

namespace DataAccess.Dao.Huntsman
{
    public interface IHuntsmanDao
    {
        IList<Entities.Huntsman> GetAll();
    }
}