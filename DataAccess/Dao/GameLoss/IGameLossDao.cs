using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.GameLoss
{
    public interface IGameLossDao
    {
        IList<GameLossDto> GetAll();
        void Insert(GameLossDto gameLoss);
    }
}