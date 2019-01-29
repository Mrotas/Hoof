using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.Huntsman
{
    public interface IHuntsmanDao
    {
        IList<HuntsmanDto> GetAll();
    }
}