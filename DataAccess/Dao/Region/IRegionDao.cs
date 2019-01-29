using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.Region
{
    public interface IRegionDao
    {
        IList<RegionDto> GetAll();
        int GetRegionId(string city, int circuit, int district);
    }
}