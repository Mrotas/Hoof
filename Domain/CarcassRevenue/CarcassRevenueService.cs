using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.CarcassRevenue;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dto;
using Domain.CarcassRevenue.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Domain.CarcassRevenue
{
    public class CarcassRevenueService : ICarcassRevenueService
    {
        private readonly ICarcassRevenueDao _carcassRevenueDao;
        private readonly IHuntDao _huntDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IGameDao _gameDao;
        private readonly IGameClassDao _gameClassDao;
        private readonly IMarketingYearService _marketingYearService;

        public CarcassRevenueService() : this(new CarcassRevenueDao(), new HuntDao(),new HuntedGameDao(), new GameDao(), new GameClassDao(), new MarketingYearService())
        {
        }

        public CarcassRevenueService(ICarcassRevenueDao carcassRevenueDao, IHuntDao huntDao, IHuntedGameDao huntedGameDao, IGameDao gameDao, IGameClassDao gameClassDao, IMarketingYearService marketingYearService)
        {
            _carcassRevenueDao = carcassRevenueDao;
            _huntDao = huntDao;
            _huntedGameDao = huntedGameDao;
            _gameDao = gameDao;
            _gameClassDao = gameClassDao;
            _marketingYearService = marketingYearService;
        }

        public CarcassRevenueBaseViewModel GetCarcassRevenueViewModel(int marketingYearId)
        {
            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(marketingYearId);

            IList<CarcassRevenueDto> carcassRevenueDtos = _carcassRevenueDao.GetByMarketingYear(marketingYearId);
            IList<HuntDto> huntDtos = _huntDao.GetByMarketingYear(marketingYearId);
            IList<HuntedGameDto> huntedGameDtos = _huntedGameDao.GetByMarketingYear(marketingYearId);
            IList<GameDto> gameDtos = _gameDao.GetAll();
            IList<GameClassDto> gameClassDtos = _gameClassDao.GetAll();

            List<CarcassRevenueViewModel> carcassRevenueViewModels =
            (
                from carcassRevenue in carcassRevenueDtos
                join huntedGame in huntedGameDtos on carcassRevenue.HuntedGameId equals huntedGame.Id
                join hunt in huntDtos on huntedGame.Id equals hunt.HuntedGameId
                join game in gameDtos on huntedGame.GameId equals game.Id
                where hunt.Date >= marketingYearModel.Start && hunt.Date <= marketingYearModel.End
                select new CarcassRevenueViewModel
                {
                    Id = carcassRevenue.Id,
                    HuntedGameId = huntedGame.Id,
                    GameKind = game.Kind,
                    GameKindName = game.KindName,
                    GameSubKind = game.SubKind,
                    GameSubKindName = game.SubKindName,
                    Class = huntedGame.GameClass,
                    ClassName = huntedGame.GameClass.HasValue ? gameClassDtos.Single(x => x.Id == huntedGame.GameClass).ClassName : "",
                    Revenue = carcassRevenue.Revenue,
                    CarcassWeight = carcassRevenue.CarcassWeight,
                    HuntDate = hunt.Date
                }
            ).ToList();

            var carcassRevenueBaseViewModel = new CarcassRevenueBaseViewModel
            {
                CarcassRevenueViewModels = carcassRevenueViewModels,
                MarketingYearModel = marketingYearModel
            };

            return carcassRevenueBaseViewModel;
        }

        public void AddCarcassRevenue(CarcassRevenueViewModel model, int marketingYearId)
        {
            IList<CarcassRevenueDto> existingCarcassRevenueDtos = _carcassRevenueDao.GetByMarketingYear(marketingYearId);
            if (existingCarcassRevenueDtos.Any(x => x.HuntedGameId == model.HuntedGameId))
            {
                throw new Exception("Podana zwierzyna została już wcześniej użyta do rozliczenia przychodu. Proszę wybrać inną zwierzynę lub edytować istniejący przychód.");
            }

            var dto = new CarcassRevenueDto
            {
                HuntedGameId = model.HuntedGameId,
                CarcassWeight = model.CarcassWeight,
                Revenue = model.Revenue
            };

            _carcassRevenueDao.Insert(dto);
        }

        public void UpdateCarcassRevenue(CarcassRevenueViewModel model, int marketingYearId)
        {
            var dto = new CarcassRevenueDto
            {
                Id = model.Id,
                HuntedGameId = model.HuntedGameId,
                CarcassWeight = model.CarcassWeight,
                Revenue = model.Revenue
            };

            _carcassRevenueDao.Update(dto);
        }

        public void DeleteCarcassRevenue(int id)
        {
            _carcassRevenueDao.Delete(id);
        }
    }
}
