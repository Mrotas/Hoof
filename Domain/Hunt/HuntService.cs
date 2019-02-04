using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.Region;
using DataAccess.Dto;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Domain.Hunt
{
    public class HuntService : IHuntService
    {
        private IList<HuntDto> _hunts;
        private IList<HuntDto> Hunts => _hunts ?? (_hunts = _huntDao.GetAll());

        private IList<HuntedGameDto> _huntedGames;
        private IList<HuntedGameDto> HuntedGames => _huntedGames ?? (_huntedGames = _huntedGameDao.GetAll());

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());

        private IList<HuntsmanDto> _huntsmans;
        private IList<HuntsmanDto> Huntsmans => _huntsmans ?? (_huntsmans = _huntsmanDao.GetAll());

        private IList<RegionDto> _regions;
        private IList<RegionDto> Regions => _regions ?? (_regions = _regionDao.GetAll());

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private readonly IHuntDao _huntDao;
        private readonly IGameDao _gameDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly IRegionDao _regionDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IGameClassDao _gameClassDao;

        public HuntService() : this (new HuntDao(), new GameDao(), new HuntsmanDao(), new RegionDao(), new HuntedGameDao(), new GameClassDao())
        {
        }

        public HuntService(IHuntDao huntDao, IGameDao gameDao, IHuntsmanDao huntsmanDao, IRegionDao regionDao, IHuntedGameDao huntedGameDao, IGameClassDao gameClassDao)
        {
            _huntDao = huntDao;
            _gameDao = gameDao;
            _huntsmanDao = huntsmanDao;
            _regionDao = regionDao;
            _huntedGameDao = huntedGameDao;
            _gameClassDao = gameClassDao;
        }

        public IList<HuntViewModel> GetAllHunts()
        {
            List<HuntViewModel> huntViewModels = 
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where game.Type == (int) GameType.Big
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameClass = GetGameClass(huntedGame.GameClass),
                    GameWeight = GetGameWeight(huntedGame.GameWeight),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public IList<HuntViewModel> GetHuntsByHuntsmanId(int huntsmanId)
        {
            List<HuntViewModel> huntViewModels = 
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where huntsman.Id == huntsmanId && game.Type == (int) GameType.Big
                select new HuntViewModel
                {
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameClass = GetGameClass(huntedGame.GameClass),
                    GameWeight = GetGameWeight(huntedGame.GameWeight),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public IList<HuntViewModel> GetAllCaughts()
        {
            List<HuntViewModel> huntViewModels =
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where game.Type == (int) GameType.Small
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameWeight = GetGameWeight(huntedGame.GameWeight),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public IList<HuntViewModel> GetCaughtsByHuntsmanId(int huntsmanId)
        {
            List<HuntViewModel> huntViewModels = 
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where huntsman.Id == huntsmanId && game.Type == (int)GameType.Small
                select new HuntViewModel
                {
                    GameKindName = game.KindName,
                    GameSubKindName = GetSubKindName(game.SubKindName),
                    GameWeight = GetGameWeight(huntedGame.GameWeight),
                    City = region.City,
                    Circuit = region.Circuit,
                    District = region.District,
                    Shots = hunt.Shots,
                    Date = hunt.Date
                }
            ).ToList();

            return huntViewModels;
        }

        public void Create(HuntCreateModel model, int huntsmanId)
        {
            GameDto game = null;
            int? huntedGameId = null;
            if (model.GameType.HasValue & model.GameKind.HasValue)
            {
                game = _gameDao.Get(model.GameType.Value, model.GameKind.Value, model.GameSubKind).FirstOrDefault();

                var huntGameDto = new HuntedGameDto
                {
                    GameId = game.Id,
                    GameClass = model.GameClass,
                    GameWeight = model.GameWeight
                };

                huntedGameId = _huntedGameDao.Insert(huntGameDto);
            }

            int regionId = _regionDao.GetRegionId(model.City, model.Circuit, model.District);

            var huntDto = new HuntDto
            {
                HuntsmanId = huntsmanId,
                HuntedGameId = huntedGameId,
                RegionId = regionId,
                Shots = model.Shots,
                Date = model.Date
            };

            _huntDao.Insert(huntDto);
        }

        private string GetSubKindName(string subKindName)
        {
            return String.IsNullOrEmpty(subKindName) ? "-" : subKindName;
        }

        private string GetGameClass(int? gameClass)
        {
            if (gameClass == null)
            {
                return "-";
            }

            GameClassDto gameClassDto = GameClassXRefs.FirstOrDefault(x => x.Id == gameClass);

            return gameClassDto.ClassName;
        }

        private string GetGameWeight(double? weight)
        {
            return weight == null ? "-" : $"{weight} kg";
        }
    }
}
