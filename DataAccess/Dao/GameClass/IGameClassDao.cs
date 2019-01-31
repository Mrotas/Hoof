using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.GameClass
{
    public interface IGameClassDao
    {
        IList<GameClassDto> GetAll();
    }
}