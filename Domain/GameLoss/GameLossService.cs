using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.Loss;
using DataAccess.Dao.LossGame;
using DataAccess.Dao.Region;
using DataAccess.Dto;
using Domain.GameLoss.Model;
using Domain.GameLoss.ViewModel;

namespace Domain.GameLoss
{
    public class GameLossService : IGameLossService
    {
        private readonly IGameDao _gameDao;
        private readonly ILossDao _lossDao;
        private readonly ILossGameDao _lossGameDao;
        private readonly IRegionDao _regionDao;

        public GameLossService() : this(new GameDao(), new LossDao(), new RegionDao(), new LossGameDao())
        {
            
        }

        public GameLossService(IGameDao gameDao, ILossDao lossDao, IRegionDao regionDao, ILossGameDao lossGameDao)
        {
            _gameDao = gameDao;
            _lossDao = lossDao;
            _regionDao = regionDao;
            _lossGameDao = lossGameDao;
        }

        public List<GameLossViewModel> GetAllLossGames()
        {
            IList<LossDto> losses = _lossDao.GetAll();
            IList<LossGameDto> lossGames = _lossGameDao.GetAll();
            IList<GameDto> games = _gameDao.GetAll();
            IList<RegionDto> regions = _regionDao.GetAll();

            List<GameLossViewModel> gameLossModels = (from loss in losses
                                                      join lossGame in lossGames on loss.LossGameId equals lossGame.Id
                                                      join game in games on lossGame.GameId equals game.Id
                                                      join region in regions on loss.RegionId equals region.Id
                                                      select new GameLossViewModel
                                                      {
                                                          TypeName = game.Type == 1 ? "Gruba" : "Drobna",
                                                          KindName = game.KindName,
                                                          SubKindName = game.SubKindName,
                                                          Circuit = region.City,
                                                          District = region.District,
                                                          Description = loss.Description,
                                                          SanitaryLoss = loss.SanitaryLoss,
                                                          Date = loss.Date
                                                      }).ToList();

            return gameLossModels;
        }

        public void ReportLoss(GameLossModel model)
        {
            IList<GameDto> gameDtos = _gameDao.Get(model.Type, model.Kind, model.SubKind);

            GameDto gameDto = gameDtos.FirstOrDefault();

            if (gameDto == null)
            {
                throw new Exception("Could not find specified game!");
            }

            int regionId = _regionDao.GetRegionId(model.City, model.Circuit, model.District);
            
            var gameLoss = new LossGameDto
            {
                GameId = gameDto.Id,
                Class = model.Class
            };

            var loss = new LossDto
            {
                RegionId = regionId,
                Description = model.Description,
                Date = model.Date,
                SanitaryLoss = model.SanitaryLoss
            };

            _lossDao.Insert(gameLoss, loss);
        }
    }
}
