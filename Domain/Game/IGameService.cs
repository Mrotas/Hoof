using System.Collections.Generic;
using Domain.Game.Model;

namespace Domain.Game
{
    public interface IGameService
    {
        List<GameModel> GetAllGames();
        List<GameModel> GetByKindName(string kindName);
    }
}