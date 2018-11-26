using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.Region;
using DataAccess.Entities;
using Domain.Hunt.Models;
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
            IList<DataAccess.Entities.Game> games = _gameDao.GetAll();
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
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameClass = GetGameClass(hunt.GameClass),
                    GameWeight = GetGameWeight(hunt.GameWeight),
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
            IList<DataAccess.Entities.Game> games = _gameDao.GetAll();
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
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameClass = GetGameClass(hunt.GameClass),
                    GameWeight = GetGameWeight(hunt.GameWeight),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }).ToList();

            return huntViewModels;
        }

        public void Create(HuntCreateModel model, int huntsmanId)
        {
            DataAccess.Entities.Game game = null;
            if (model.GameType.HasValue & model.GameKind.HasValue)
            {
                game = _gameDao.Get(model.GameType.Value, model.GameKind.Value, model.GameSubKind).FirstOrDefault();
            }

            int regionId = _regionDao.GetRegionId(model.City, model.Circuit, model.District);

            var hunt = new DataAccess.Entities.Hunt
            {
                HuntsmanId = huntsmanId,
                GameId = game.Id,
                GameClass = model.GameClass,
                GameWeight= model.GameWeight,
                RegionId = regionId,
                Shots = model.Shots,
                Date = DateTime.Today
            };

            _huntDao.Insert(hunt);
        }
        
        private string GetGameType(int? gameType)
        {
            if (gameType == null)
            {
                throw new Exception("Nie można znaleźć typu zwierzyny!");
            }
            
            return gameType == 1 ? "Gruba" : "Drobna";
        }

        private string GetSubKindName(string subKindName)
        {
            if (String.IsNullOrEmpty(subKindName))
            {
                return "-";
            }

            return subKindName;
        }

        private string GetGameClass(int? gameClass)
        {
            if (gameClass == null)
            {
                return "-";
            }

            switch (gameClass)
            {
                case 1: return "I";
                case 2: return "II";
                case 3: return "III";
                default: return "-";
            }
        }

        private string GetGameWeight(double? weight)
        {
            if (weight == null)
            {
                return "-";
            }

            return weight.ToString();
        }
    }
}
