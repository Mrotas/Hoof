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
                    GameKindName = game.KindName,
                    GameSubKindName = game.SubKindName,
                    GameClassName = huntPlan.Class.HasValue ? GameClassXRefs.FirstOrDefault(x => x.Id == huntPlan.Class).ClassName : String.Empty,
                    Class = huntPlan.Class,
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

    }
}
