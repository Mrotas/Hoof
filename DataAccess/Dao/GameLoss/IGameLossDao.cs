using System.Collections.Generic;

namespace DataAccess.Dao.GameLoss
{
    public interface IGameLossDao
    {
        List<Entities.GameLoss> GetAll();
        void Insert(Entities.GameLoss gameLoss);
    }
}