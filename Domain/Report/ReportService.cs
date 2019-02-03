using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dto;
using Domain.MarketingYear;
using Domain.Report.Models;

namespace Domain.Report
{
    public class ReportService : IReportService
    {
        private IList<HuntDto> _hunts;
        private IList<HuntedGameDto> _huntedGames;
        private IList<GameDto> _games;
        private IList<GameClassDto> _gameClasses;

        private readonly IHuntDao _huntDao;
        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IGameClassDao _gameClassDao;

        public ReportService() : this(new HuntDao(), new GameDao(), new GameHuntPlanDao(), new HuntedGameDao(), new MarketingYearService(), new GameClassDao())
        {
            _hunts = new List<HuntDto>();
            _huntedGames = new List<HuntedGameDto>();
            _games = new List<GameDto>();
            _gameClasses = new List<GameClassDto>();
        }

        public ReportService(IHuntDao huntDao, IGameDao gameDao, IGameHuntPlanDao gameHuntPlanDao, IHuntedGameDao huntedGameDao, IMarketingYearService marketingYearService, IGameClassDao gameClassDao)
        {
            _huntDao = huntDao;
            _gameDao = gameDao;
            _gameHuntPlanDao = gameHuntPlanDao;
            _huntedGameDao = huntedGameDao;
            _marketingYearService = marketingYearService;
            _gameClassDao = gameClassDao;
        }

        public List<MonthlyReportModel> GetMonthlyReportData(DateTime startDate, DateTime endDate)
        {
            _hunts = _huntDao.GetHuntsByDateRange(startDate, endDate);
            _huntedGames = _huntedGameDao.GetAll();
            _games = _gameDao.GetAll();
            _gameClasses = _gameClassDao.GetAll();

            int marketingYearId = _marketingYearService.GetMarketingYearByDate(startDate);

            IList<GameHuntPlanDto> gameHuntPlans = _gameHuntPlanDao.GetByMarketingYear(marketingYearId);

            var monthlyReportModels = new List<MonthlyReportModel>();
            foreach (GameHuntPlanDto gameHuntPlan in gameHuntPlans)
            {
                MonthlyReportModel model = GetMonthlyReportModel(gameHuntPlan);
                monthlyReportModels.Add(model);
            }

            return monthlyReportModels;
        }

        private MonthlyReportModel GetMonthlyReportModel(GameHuntPlanDto gameHuntPlan)
        {
            //int culls = _hunts.Count(x => x.GameId == gameHuntPlan.GameId);

            int culls = (from hunt in _hunts
                        join huntedGame in _huntedGames on hunt.HuntedGameId equals huntedGame.Id
                        where gameHuntPlan.GameId == huntedGame.Id
                        select hunt.Id).Count();

            int planned = 0;
            string gameClassName = null;

            GameDto game = _games.FirstOrDefault(x => x.Id == gameHuntPlan.GameId);
            if (gameHuntPlan?.Cull != null)
            {
                planned = gameHuntPlan.Cull.Value;

                gameClassName = GetClassName(gameHuntPlan.Class);
            }
            
            //TODO: Fill Catch and Loss properties
            var monthlyReportModel = new MonthlyReportModel
            {
                GameKindName = game.KindName,
                GameSubKindName = game.SubKindName,
                GameClass = gameClassName,
                Cull = culls,
                Catch = 0,
                Loss = 0,
                Planned = planned
            };

            return monthlyReportModel;
        }

        private string GetClassName(int? gameClass)
        {
            if (gameClass == null)
            {
                return String.Empty;
            }

            string className = _gameClasses.FirstOrDefault(x => x.Id == gameClass).ClassName;
            return className;
        }
    }
}
