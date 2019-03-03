using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.GameHuntPlan.ViewModels;

namespace Domain.GameHuntPlan
{
    public class GameHuntPlanService : IGameHuntPlanService
    {
        private int MarketingYearId { get; set; }
        
        private IList<GameHuntPlanDto> _gameHuntPlans;
        private IList<GameHuntPlanDto> GameHuntPlans => _gameHuntPlans ?? (_gameHuntPlans = _gameHuntPlanDao.GetByMarketingYear(MarketingYearId));

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private readonly IMarketingYearDao _marketingYearDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IGameDao _gameDao;
        private readonly IGameClassDao _gameClassDao;

        public GameHuntPlanService() : this(new MarketingYearDao(), new GameHuntPlanDao(), new GameDao(), new GameClassDao())
        {
        }

        public GameHuntPlanService(IMarketingYearDao marketingYearDao, IGameHuntPlanDao gameHuntPlanDao, IGameDao gameDao, IGameClassDao gameClassDao)
        {
            _marketingYearDao = marketingYearDao;
            _gameHuntPlanDao = gameHuntPlanDao;
            _gameDao = gameDao;
            _gameClassDao = gameClassDao;
        }

        public HuntPlanViewModel GetHuntPlanViewModel(int marketingYearId)
        {
            MarketingYearId = marketingYearId;

            List<GameHuntPlanViewModel> gameHuntPlanViewModels = 
            (
                from huntPlan in GameHuntPlans
                join game in Games on huntPlan.GameId equals game.Id
                where huntPlan.MarketingYearId == MarketingYearId
                select new GameHuntPlanViewModel
                {
                    Id = huntPlan.Id,
                    GameId = game.Id,
                    GameType = game.Type,
                    GameKind = game.Kind,
                    GameKindName = game.KindName,
                    GameSubKind = game.SubKind,
                    GameSubKindName = game.SubKindName,
                    Class = huntPlan.Class,
                    ClassName = huntPlan.Class.HasValue ? GameClassXRefs.FirstOrDefault(x => x.Id == huntPlan.Class).ClassName : String.Empty,
                    Cull = huntPlan.Cull,
                    Catch = huntPlan.Catch
                }
            ).ToList();

            DataAccess.Entities.MarketingYear marketingYear = _marketingYearDao.GetById(MarketingYearId);

            var huntPlanViewModel = new HuntPlanViewModel
            {
                MarketingYearId = marketingYear.Id,
                MarketingYearStart = marketingYear.Start,
                MarketingYearEnd = marketingYear.End,
                GameHuntPlanViewModels = gameHuntPlanViewModels
            };

            return huntPlanViewModel;
        }

        public void AddGameHuntPlan(GameHuntPlanViewModel model, int marketingYearId)
        {
            IList<GameDto> games = _gameDao.GetByKindName(model.GameKindName);
            if (!String.IsNullOrWhiteSpace(model.GameSubKindName))
            {
                games = games.Where(x => x.SubKindName == model.GameSubKindName).ToList();
            }

            int gameId = games.Select(x => x.Id).FirstOrDefault();
            IList<GameHuntPlanDto> existingHuntPlanDto = _gameHuntPlanDao.GetByMarketingYear(marketingYearId);
            if (existingHuntPlanDto.Any(x => x.GameId == gameId && x.Class == model.Class))
            {
                throw new Exception($"Plan pozyskania zwierzyny {model.GameKindName} - {model.GameSubKindName} - {model.ClassName} już istnieje! Proszę użyć opcji edycji istniejącego już planu.");
            }

            var dto = new GameHuntPlanDto
            {
                GameId = gameId,
                Class = model.Class,
                Cull = model.Cull,
                Catch = model.Catch,
                MarketingYearId = marketingYearId
            };

            _gameHuntPlanDao.Insert(dto);
        }

        public void UpdateGameHuntPlan(GameHuntPlanViewModel model, int marketingYearId)
        {
            var dto = new GameHuntPlanDto
            {
                Id = model.Id,
                GameId = model.GameId,
                Class = model.Class,
                Cull = model.Cull,
                Catch = model.Catch,
                MarketingYearId = marketingYearId
            };

            _gameHuntPlanDao.Update(dto);
        }

        public void DeleteGameHuntPlan(int id)
        {
            _gameHuntPlanDao.Delete(id);
        }
    }
}
