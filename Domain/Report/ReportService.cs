using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.GameLoss;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.MarketingYear;
using DataAccess.Dto;
using Domain.MarketingYear;
using Domain.Report.Models;

namespace Domain.Report
{
    public class ReportService : IReportService
    {
        private IList<HuntDto> _hunts;
        private IList<HuntedGameDto> _huntedGames;
        private IList<GameLossDto> _lossGames;
        private IList<GameDto> _games;

        private readonly IHuntDao _huntDao;
        private readonly IGameDao _gameDao;
        private readonly IGameLossDao _gameLossDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IMarketingYearService _marketingYearService;

        public ReportService() : this(new HuntDao(), new GameDao(), new GameLossDao(), new GameHuntPlanDao(), new HuntedGameDao(), new MarketingYearService())
        {
            
        }

        public ReportService(IHuntDao huntDao, IGameDao gameDao, IGameLossDao gameLossDao, IGameHuntPlanDao gameHuntPlanDao, IHuntedGameDao huntedGameDao, IMarketingYearService marketingYearService)
        {
            _huntDao = huntDao;
            _gameDao = gameDao;
            _gameLossDao = gameLossDao;
            _gameHuntPlanDao = gameHuntPlanDao;
            _huntedGameDao = huntedGameDao;
            _marketingYearService = marketingYearService;
        }

        public List<MonthlyReportModel> GetMonthlyReportData(DateTime startDate, DateTime endDate)
        {
            _hunts = _huntDao.GetHuntsByDateRange(startDate, endDate);
            _huntedGames = _huntedGameDao.GetAll();
            _lossGames = _gameLossDao.GetAll();
            _games = _gameDao.GetAll();

            int marketingYearId = _marketingYearService.GetMarketingYearByDate(startDate);

            IList<GameHuntPlanDto> gameHuntPlans = _gameHuntPlanDao.GetGameHuntPlan(marketingYearId);

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

            int losses = _lossGames.Count(x => x.GameId == gameHuntPlan.GameId);
            int planned = 0;
            string gameClassName = null;

            GameDto game = _games.FirstOrDefault(x => x.Id == gameHuntPlan.GameId);
            if (gameHuntPlan?.Cull != null)
            {
                planned = gameHuntPlan.Cull.Value;

                gameClassName = GetGameClassName(gameHuntPlan.Class);
            }
            
            //TODO: Fill Catch property
            var monthlyReportModel = new MonthlyReportModel
            {
                GameKindName = game.KindName,
                GameSubKindName = game.SubKindName,
                GameClass = gameClassName,
                Cull = culls,
                Catch = 0,
                Loss = losses,
                Planned = planned
            };

            return monthlyReportModel;
        }

        private string GetGameClassName(int? classNumber)
        {
            if (!classNumber.HasValue)
            {
                return null;
            }

            return classNumber.Value == 1 ? "selekcyjne" : "łowne";
        }
    }
}
