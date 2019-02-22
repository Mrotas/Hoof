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
                    GameKindName = game.KindName,
                    GameSubKindName = game.SubKindName,
                    GameClassName = countPlan.Class.HasValue ? GameClassXRefs.FirstOrDefault(x => x.Id == countPlan.Class).ClassName : String.Empty,
                    Class = countPlan.Class,
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
    }
}
