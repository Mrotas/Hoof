using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Catch;
using DataAccess.Dao.Game;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.Region;
using DataAccess.Dto;
using Domain.Catch.Models;
using Domain.Catch.ViewModels;

namespace Domain.Catch
{
    public class CatchService : ICatchService
    {
        private IList<CatchDto> _catches;
        private IList<CatchDto> Catches => _catches ?? (_catches = _catchDao.GetAll());

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());

        private IList<HuntsmanDto> _huntsmans;
        private IList<HuntsmanDto> Huntsmans => _huntsmans ?? (_huntsmans = _huntsmanDao.GetAll());

        private IList<RegionDto> _regions;
        private IList<RegionDto> Regions => _regions ?? (_regions = _regionDao.GetAll());
        
        private readonly ICatchDao _catchDao;
        private readonly IGameDao _gameDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly IRegionDao _regionDao;

        public CatchService() : this(new CatchDao(), new GameDao(), new HuntsmanDao(), new RegionDao())
        {
        }

        public CatchService(ICatchDao catchDao, IGameDao gameDao, IHuntsmanDao huntsmanDao, IRegionDao regionDao)
        {
            _catchDao = catchDao;
            _gameDao = gameDao;
            _huntsmanDao = huntsmanDao;
            _regionDao = regionDao;
        }

        public IList<CatchViewModel> GetAllCatches()
        {
            List<CatchViewModel> huntViewModels =
            (
                from gameCatch in Catches
                join huntsman in Huntsmans on gameCatch.HuntsmanId equals huntsman.Id
                join game in Games on gameCatch.GameId equals game.Id
                join region in Regions on gameCatch.RegionId equals region.Id
                select new CatchViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Count = gameCatch.Count,
                    Date = gameCatch.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public IList<CatchViewModel> GetCatchesByHuntsmanId(int huntsmanId)
        {
            List<CatchViewModel> huntViewModels =
            (
                from gameCatch in Catches
                join huntsman in Huntsmans on gameCatch.HuntsmanId equals huntsman.Id
                join game in Games on gameCatch.GameId equals game.Id
                join region in Regions on gameCatch.RegionId equals region.Id
                where gameCatch.HuntsmanId == huntsmanId
                select new CatchViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Count = gameCatch.Count,
                    Date = gameCatch.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public void Create(CatchCreateModel model, int huntsmanId)
        {
            GameDto game = _gameDao.Get(model.GameType, model.GameKind, model.GameSubKind).FirstOrDefault();
            if (game == null)
            {
                throw new Exception($"Could not find game by specified parameters: Type={model.GameType}, Kind={model.GameKind}, SubKind={model.GameSubKind}");
            }

            int regionId = _regionDao.GetRegionId(model.City, model.Circuit, model.District);

            var catchDto = new CatchDto
            {
                HuntsmanId = huntsmanId,
                GameId = game.Id,
                RegionId = regionId,
                Count = model.Count,
                Date = model.Date
            };

            _catchDao.Insert(catchDto);
        }

        private string GetSubKindName(string subKindName)
        {
            return String.IsNullOrEmpty(subKindName) ? "-" : subKindName;
        }
    }
}
