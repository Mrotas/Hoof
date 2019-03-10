using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameCountFor10March;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.GameCountPlan.ViewModels;

namespace Domain.GameCountPlan
{
    public class GameCountPlanService : IGameCountPlanService
    {
        private int MarketingYearId { get; set; }

        private IList<GameCountFor10MarchDto> _gameCountPlans;
        private IList<GameCountFor10MarchDto> GameCountPlans => _gameCountPlans ?? (_gameCountPlans = _gameCountFor10MarchDao.GetByMarketingYear(MarketingYearId));

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private readonly IGameCountFor10MarchDao _gameCountFor10MarchDao;
        private readonly IGameDao _gameDao;
        private readonly IGameClassDao _gameClassDao;
        private readonly IMarketingYearDao _marketingYearDao;

        public GameCountPlanService() : this(new GameCountFor10MarchDao(), new GameDao(), new GameClassDao(), new MarketingYearDao())
        {
        }

        public GameCountPlanService(IGameCountFor10MarchDao gameCountFor10MarchDao, IGameDao gameDao, IGameClassDao gameClassDao, IMarketingYearDao marketingYearDao)
        {
            _gameCountFor10MarchDao = gameCountFor10MarchDao;
            _gameDao = gameDao;
            _gameClassDao = gameClassDao;
            _marketingYearDao = marketingYearDao;
        }

        public CountPlanViewModel GetCountPlanViewModel(int marketingYearId)
        {
            MarketingYearId = marketingYearId;

            List<GameCountPlanViewModel> gameCountPlanViewModels =
            (
                from countPlan in GameCountPlans
                join game in Games on countPlan.GameId equals game.Id
                where countPlan.MarketingYearId == MarketingYearId
                select new GameCountPlanViewModel
                {
                    Id = countPlan.Id,
                    GameId = game.Id,
                    GameType = game.Type,
                    GameKind = game.Kind,
                    GameKindName = game.KindName,
                    GameSubKind = game.SubKind,
                    GameSubKindName = game.SubKindName,
                    Class = countPlan.Class,
                    ClassName = countPlan.Class.HasValue ? GameClassXRefs.FirstOrDefault(x => x.Id == countPlan.Class).ClassName : String.Empty,
                    Count = countPlan.Count
                }
            ).ToList();

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(MarketingYearId);

            var huntPlanViewModel = new CountPlanViewModel
            {
                MarketingYearId = marketingYear.Id,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End,
                GameCountPlanViewModels = gameCountPlanViewModels
            };

            return huntPlanViewModel;
        }

        public void AddGameHuntPlan(GameCountPlanViewModel model, int marketingYearId)
        {
            IList<GameDto> games = _gameDao.GetByKindName(model.GameKindName);
            if (!String.IsNullOrWhiteSpace(model.GameSubKindName))
            {
                games = games.Where(x => x.SubKindName == model.GameSubKindName).ToList();
            }

            int gameId = games.Select(x => x.Id).FirstOrDefault();
            IList<GameCountFor10MarchDto> existingGameCountPlanDto = _gameCountFor10MarchDao.GetByMarketingYear(marketingYearId);
            if (existingGameCountPlanDto.Any(x => x.GameId == gameId && x.Class == model.Class))
            {
                throw new Exception($"Plan liczebności zwierzyny {model.GameKindName} - {model.GameSubKindName} - {model.ClassName} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new GameCountFor10MarchDto
            {
                GameId = gameId,
                Class = model.Class,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _gameCountFor10MarchDao.Insert(dto);
        }

        public void UpdateGameHuntPlan(GameCountPlanViewModel model, int marketingYearId)
        {
            var dto = new GameCountFor10MarchDto
            {
                Id = model.Id,
                GameId = model.GameId,
                Class = model.Class,
                Count = model.Count,
                MarketingYearId = marketingYearId
            };

            _gameCountFor10MarchDao.Update(dto);
        }

        public void DeleteGameHuntPlan(int id)
        {
            _gameCountFor10MarchDao.Delete(id);
        }
    }
}
