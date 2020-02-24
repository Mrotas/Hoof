using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.Huntsman
{
    public interface IHuntsmanDao
    {
        HuntsmanDto GetById(int huntsmanId);
        IList<HuntsmanDto> GetAll();
        int Insert(HuntsmanDto dto);
    }
}