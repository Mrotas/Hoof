using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dto;
using Domain.Game.Model;

namespace Domain.Game
{
    public class GameService : IGameService
    {
        private readonly IGameDao _gameDao;

        public GameService(): this (new GameDao())
        {
            
        }

        public GameService(IGameDao gameDao)
        {
            _gameDao = gameDao;
        }

        public List<GameModel> GetAllGames()
        {
            IList<GameDto> games = _gameDao.GetAll();

            var gameModels = games.Select(x => new GameModel
            {
                Id = x.Id,
                Type = x.Type,
                Kind = x.Kind,
                KindName = x.KindName,
                SubKind = x.SubKind,
                SubKindName = x.SubKindName
            }).ToList();

            return gameModels;
        }
    }
}
