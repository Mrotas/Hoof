using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameLoss;
using DataAccess.Dao.Region;
using DataAccess.Dto;
using Domain.GameLoss.Model;
using Domain.GameLoss.ViewModel;

namespace Domain.GameLoss
{
    public class GameLossService : IGameLossService
    {
        private readonly IGameDao _gameDao;
        private readonly IGameLossDao _gameLossDao;
        private readonly IRegionDao _regionDao;

        public GameLossService() : this(new GameDao(), new GameLossDao(), new RegionDao())
        {
            
        }

        public GameLossService(IGameDao gameDao, IGameLossDao gameLossDao, IRegionDao regionDao)
        {
            _gameDao = gameDao;
            _gameLossDao = gameLossDao;
            _regionDao = regionDao;
        }

        public List<GameLossViewModel> GetAllLossGames()
        {
            IList<GameLossDto> lossGames = _gameLossDao.GetAll();
            IList<GameDto> games = _gameDao.GetAll();
            IList<RegionDto> regions = _regionDao.GetAll();

            List<GameLossViewModel> gameLossModels = (from gameLoss in lossGames
                                                join game in games on gameLoss.GameId equals game.Id
                                                join region in regions on gameLoss.RegionId equals region.Id
                                                select new GameLossViewModel
                                                {
                                                    TypeName = game.Type == 1 ? "Gruba" : "Drobna",
                                                    KindName = game.KindName,
                                                    SubKindName = game.SubKindName,
                                                    Circuit = region.City,
                                                    District = region.District,
                                                    Description = gameLoss.Description,
                                                    SanitaryLoss = gameLoss.SanitaryLoss,
                                                    Date = gameLoss.Date
                                                }).ToList();

            return gameLossModels;
        }

        public void ReportLoss(GameLossModel model)
        {
            IList<GameDto> gameDtos = _gameDao.Get(model.GameType, model.GameKind, model.GameSubKind);

            GameDto gameDto = gameDtos.FirstOrDefault();

            if (gameDto == null)
            {
                throw new Exception("Could not find specified game!");
            }

            int regionId = _regionDao.GetRegionId(model.City, model.Circuit, model.District);

            var gameLoss = new GameLossDto
            {
                GameId = gameDto.Id,
                SanitaryLoss = model.SanitaryLoss,
                RegionId = regionId,
                Description = model.Description,
                Date = model.Date
            };

            _gameLossDao.Insert(gameLoss);
        }
    }
}
