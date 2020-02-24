using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.Huntsman;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dao.Region;
using DataAccess.Dao.User;
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

        private IList<UserDto> _users;
        private IList<UserDto> Users => _users ?? (_users = _userDao.GetAll());

        private IList<HuntsmanDto> _huntsmans;
        private IList<HuntsmanDto> Huntsmans => _huntsmans ?? (_huntsmans = _huntsmanDao.GetAll());

        private IList<RegionDto> _regions;
        private IList<RegionDto> Regions => _regions ?? (_regions = _regionDao.GetAll());

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private readonly IHuntDao _huntDao;
        private readonly IGameDao _gameDao;
        private readonly IUserDao _userDao;
        private readonly IHuntsmanDao _huntsmanDao;
        private readonly IRegionDao _regionDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IGameClassDao _gameClassDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public HuntService() : this (new HuntDao(), new GameDao(), new UserDao(), new HuntsmanDao(), new RegionDao(), new HuntedGameDao(), new GameClassDao(), new MarketingYearDao())
        {
        }

        public HuntService(IHuntDao huntDao, IGameDao gameDao, IUserDao userDao, IHuntsmanDao huntsmanDao, IRegionDao regionDao, IHuntedGameDao huntedGameDao, IGameClassDao gameClassDao, IMarketingYearDao marketingYearDao)
        {
            _huntDao = huntDao;
            _gameDao = gameDao;
            _userDao = userDao;
            _huntsmanDao = huntsmanDao;
            _regionDao = regionDao;
            _huntedGameDao = huntedGameDao;
            _gameClassDao = gameClassDao;
            _marketingYearDao = marketingYearDao;
        }

        public IList<HuntViewModel> GetAllHuntsForCurrentMarketingYear()
        {
            MarketingYearDto marketingYear = _marketingYearDao.GetCurrent();
            List<HuntViewModel> huntViewModels = 
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where hunt.Date >= marketingYear.Start && hunt.Date <= marketingYear.End
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    HuntedGameId = huntedGame.Id,
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

        public IList<HuntViewModel> GetByMarketingYearId(int marketingYearId)
        {
            MarketingYearDto marketingYear = _marketingYearDao.GetById(marketingYearId);
            List<HuntViewModel> huntViewModels =
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where hunt.Date >= marketingYear.Start && hunt.Date <= marketingYear.End
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    HuntedGameId = huntedGame.Id,
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

        public IList<HuntViewModel> GetCurrentMarketingYearHuntsByUserId(int userId)
        {
            MarketingYearDto marketingYear = _marketingYearDao.GetCurrent();
            List<HuntViewModel> huntViewModels = 
            (
                from hunt in Hunts
                join huntsman in Huntsmans on hunt.HuntsmanId equals huntsman.Id
                join user in Users on huntsman.Id equals user.HuntsmanId
                join huntedGame in HuntedGames on hunt.HuntedGameId equals huntedGame.Id
                join game in Games on huntedGame.GameId equals game.Id
                join region in Regions on hunt.RegionId equals region.Id
                where user.Id == userId &&
                      hunt.Date >= marketingYear.Start && hunt.Date <= marketingYear.End
                select new HuntViewModel
                {
                    HuntsmanName = huntsman.Name,
                    HuntsmanLastName = huntsman.LastName,
                    HuntedGameId = huntedGame.Id,
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
        
        public void Create(HuntCreateModel model, int userId)
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

            UserDto user = _userDao.GetById(userId);

            var huntDto = new HuntDto
            {
                HuntsmanId = user.HuntsmanId,
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
