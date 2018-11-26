using System.Collections.Generic;

namespace DataAccess.Dao.Region
{
    public interface IRegionDao
    {
        IList<Entities.Region> GetAll();
        int GetRegionId(string city, int circuit, int district);
    }
}