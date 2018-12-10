using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameLoss;
using Domain.GameLoss.Model;
using Domain.GameLoss.ViewModel;

namespace Domain.GameLoss
{
    public class GameLossService : IGameLossService
    {
        private readonly IGameDao _gameDao;
        private readonly IGameLossDao _gameLossDao;

        public GameLossService() : this(new GameDao(), new GameLossDao())
        {
            
        }

        public GameLossService(IGameDao gameDao, IGameLossDao gameLossDao)
        {
            _gameDao = gameDao;
            _gameLossDao = gameLossDao;
        }

        public List<GameLossViewModel> GetAllLossGames()
        {
            List<DataAccess.Entities.GameLoss> lossGames = _gameLossDao.GetAll();
            IList<DataAccess.Entities.Game> games = _gameDao.GetAll();

            List<GameLossViewModel> gameLossModels = (from gameLoss in lossGames
                                                join game in games on gameLoss.GameId equals game.Id
                                                select new GameLossViewModel
                                                {
                                                    TypeName = game.Type == 1 ? "Gruba" : "Drobna",
                                                    KindName = game.KindName,
                                                    SubKindName = game.SubKindName,
                                                    Circuit = gameLoss.City,
                                                    District = gameLoss.District,
                                                    Description = gameLoss.Description,
                                                    SanitaryLoss = gameLoss.SanitaryLoss,
                                                    Date = gameLoss.Date
                                                }).ToList();

            return gameLossModels;
        }

        public void ReportLoss(GameLossModel model)
        {
            DataAccess.Entities.Game game = _gameDao.Get(model.GameType, model.GameKind, model.GameSubKind).FirstOrDefault();

            var gameLoss = new DataAccess.Entities.GameLoss
            {
                GameId = game.Id,
                SanitaryLoss = model.SanitaryLoss,
                City = model.City,
                Circuit = model.Circuit,
                District = model.District,
                Description = model.Description,
                Date = model.Date
            };

            _gameLossDao.Insert(gameLoss);
        }
    }
}
