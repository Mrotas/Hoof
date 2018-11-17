using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.Region;
using DataAccess.Entities;
using Domain.Hunt.ViewModels;

namespace Domain.Hunt
{
    public class HuntService : IHuntService
    {
        private readonly IHuntDao _huntDao;
        private readonly IGameDao _gameDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly IRegionDao _regionDao;

        public HuntService() : this (new HuntDao(), new GameDao(), new HuntsmanDao(), new RegionDao())
        {
        }

        public HuntService(IHuntDao huntDao, IGameDao gameDao, IHuntsmanDao huntsmanDao, IRegionDao regionDao)
        {
            _huntDao = huntDao;
            _gameDao = gameDao;
            _huntsmanDao = huntsmanDao;
            _regionDao = regionDao;
        }

        public IList<HuntViewModel> GetAllHuntModels()
        {
            IList<DataAccess.Entities.Hunt> hunts = _huntDao.GetAll();
            IList<Game> games = _gameDao.GetAll();
            IList<Huntsman> huntsmans = _huntsmanDao.GetAll();
            IList<Region> regions = _regionDao.GetAll();

            List<HuntViewModel> huntViewModels = (from hunt in hunts
                join huntsman in huntsmans on hunt.HuntsmanId equals huntsman.Id
                join game in games on hunt.GameId equals game.Id
                join region in regions on hunt.RegionId equals region.Id
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    GameType = GetGameType(game?.Type),
                    GameSubType = GetGameName(game?.Type, game?.SubType),
                    GameClass = GetGameClass(game?.Class),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }).ToList();

            return huntViewModels;
        }

        public IList<HuntViewModel> GetHuntViewModels(int huntsmanId)
        {
            IList<DataAccess.Entities.Hunt> hunts = _huntDao.GetAll();
            IList<Game> games = _gameDao.GetAll();
            IList<Huntsman> huntsmans = _huntsmanDao.GetAll();
            IList<Region> regions = _regionDao.GetAll();

            List<HuntViewModel> huntViewModels = (from hunt in hunts
                join huntsman in huntsmans on hunt.HuntsmanId equals huntsman.Id
                join game in games on hunt.GameId equals game.Id
                join region in regions on hunt.RegionId equals region.Id
                where huntsman.Id == huntsmanId
                select new HuntViewModel
                {
                    GameType = GetGameType(game?.Type),
                    GameSubType = GetGameName(game?.Type, game?.SubType),
                    GameClass = GetGameClass(game?.Class),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }).ToList();

            return huntViewModels;
        }

        private string GetGameType(int? gameType)
        {
            if (gameType == null)
            {
                return "-";
            }

            //TODO: Get value from enum
            return gameType == 4 ? "Gruba" : "Drobna";
        }

        private string GetGameName(int? type, int? subType)
        {
            if (type == null || subType == null)
            {
                return "-";
            }

            //TODO: Get value from enum
            return "Dzik";
        }
        private string GetGameClass(int? gameClass)
        {
            if (gameClass == null)
            {
                return "-";
            }

            return gameClass.ToString();
        }
    }
}
