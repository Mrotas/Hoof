using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Extensions;
using DataAccess.Dao.Catch;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.LossGame;
using DataAccess.Dto;
using Domain.MarketingYear;
using Domain.Report.Models;

namespace Domain.Report
{
    public class ReportService : IReportService
    {
        private DateTime ReportDateFrom { get; set; }
        private DateTime ReportDateTo { get; set; }
        private int MarketingYearId { get; set; }
        private int ReportGameType { get; set; }

        private IList<GameHuntPlanDto> _huntPlans;
        private IList<GameHuntPlanDto> HuntPlans => _huntPlans ?? (_huntPlans = _gameHuntPlanDao.GetByMarketingYear(MarketingYearId));

        private IList<HuntedGameDto> _huntedGames;
        private IList<HuntedGameDto> HuntedGames => _huntedGames ?? (_huntedGames = _huntedGameDao.GetByDateRange(ReportDateFrom, ReportDateTo));

        private IList<CatchDto> _caughtGames;
        private IList<CatchDto> CaughtGames => _caughtGames ?? (_caughtGames = _catchDao.GetByMarketingYear(MarketingYearId));

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());
        private IList<GameDto> GamesByType => Games.Where(x => x.Type == ReportGameType).ToList();

        private IList<LossGameDto> _lossGames;
        private IList<LossGameDto> LossGames => _lossGames ?? (_lossGames = _lossGameDao.GetByMarketingYear(MarketingYearId));

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IGameClassDao _gameClassDao;
        private readonly ILossGameDao _lossGameDao;
        private readonly ICatchDao _catchDao;

        public ReportService() : this(new GameDao(), new GameHuntPlanDao(), new HuntedGameDao(), new MarketingYearService(), new GameClassDao(), new LossGameDao(), new CatchDao())
        {
        }

        public ReportService(IGameDao gameDao, 
            IGameHuntPlanDao gameHuntPlanDao, 
            IHuntedGameDao huntedGameDao, 
            IMarketingYearService marketingYearService, 
            IGameClassDao gameClassDao, 
            ILossGameDao lossGameDao, 
            ICatchDao catchDao)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gameHuntPlanDao;
            _huntedGameDao = huntedGameDao;
            _marketingYearService = marketingYearService;
            _gameClassDao = gameClassDao;
            _lossGameDao = lossGameDao;
            _catchDao = catchDao;
        }

        public MonthlyReportModel GetMonthlyReportData(DateTime startDate, DateTime endDate)
        {
            var monthlyReportModel = new MonthlyReportModel
            {
                MonthlyReportBigGameModel = GetMonthlyReportData(GameType.Big, startDate, endDate),
                MonthlyReportSmallGameModel = GetMonthlyReportData(GameType.Small, startDate, endDate)
            };

            return monthlyReportModel;
        }

        public MonthlyReportGameModel GetMonthlyReportData(GameType gameType, DateTime startDate, DateTime endDate)
        {
            ReportDateFrom = startDate;
            ReportDateTo = endDate;
            MarketingYearId = _marketingYearService.GetMarketingYearByDate(startDate);
            ReportGameType = (int) gameType;
            
            var gamesByKind = GamesByType.GroupBy(x => x.Kind);

            var monthlyReportKindGameModels = new List<MonthlyReportKindGameModel>();
            foreach (var gameByKind in gamesByKind)
            {
                if (!HuntPlans.Any(x => x.GameId == gameByKind.FirstOrDefault().Id))
                {
                    continue;
                }

                if (ReportGameType == (int) GameType.Big && gameByKind.Key.IsIn(new List<GameKind>{GameKind.Moose, GameKind.DeerSika}))
                {
                    continue;
                }

                MonthlyReportKindGameModel kindGameModel = GetKindMonthlyReportModel(gameByKind);

                monthlyReportKindGameModels.Add(kindGameModel);
            }

            var monthlyReportModel = new MonthlyReportGameModel
            {
                MonthlyReportKindGameModels = monthlyReportKindGameModels
            };

            return monthlyReportModel;
        }

        private MonthlyReportKindGameModel GetKindMonthlyReportModel(IGrouping<int, GameDto> gamesByKind)
        {
            var monthlyReportSubKindGameModels = new List<MonthlyReportSubKindGameModel>();

            foreach (var subKind in gamesByKind.GroupBy(x => x.SubKind))
            {
                if (subKind.Key.HasValue)
                {
                    MonthlyReportSubKindGameModel subKindGameModel = GetSubKindMonthlyReportModel(gamesByKind.Key, subKind);

                    monthlyReportSubKindGameModels.Add(subKindGameModel);
                }
            }

            var monthlyReportKindGameModel = new MonthlyReportKindGameModel
            {
                Kind = gamesByKind.Key,
                KindName = GamesByType.FirstOrDefault(x => x.Kind == gamesByKind.Key).KindName,
                MonthlyReportSubKindGameModels = monthlyReportSubKindGameModels
            };

            List<int> gameIds = GamesByType.Where(x => x.Kind == gamesByKind.Key).Select(x => x.Id).ToList();

            monthlyReportKindGameModel = SetPlans(monthlyReportKindGameModel, gameIds);

            return monthlyReportKindGameModel;
        }

        private MonthlyReportSubKindGameModel GetSubKindMonthlyReportModel(int? gameKind, IGrouping<int?, GameDto> gamesBySubKind)
        {
            if (!gamesBySubKind.Key.HasValue)
            {
                return new MonthlyReportSubKindGameModel();
            }

            List<GameHuntPlanDto> classHuntPlans = (from game in GamesByType
                                                    join huntPlan in HuntPlans on game.Id equals huntPlan.GameId
                                                    where game.Kind == gameKind && game.SubKind == gamesBySubKind.Key && huntPlan.Class != null
                                                    select huntPlan).ToList();

            var monthlyReportClassGameModels = new List<MonthlyReportClassGameModel>();
            foreach (GameHuntPlanDto classHuntPlan in classHuntPlans)
            {
                MonthlyReportClassGameModel monthlyReportClassGameModel = GetClassMonthlyReportModel(classHuntPlan);

                monthlyReportClassGameModels.Add(monthlyReportClassGameModel);
            }

            var monthlyReportSubKindGameModel = new MonthlyReportSubKindGameModel
            {
                SubKind = gamesBySubKind.Key,
                SubKindName = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == gamesBySubKind.Key).SubKindName,
                MonthlyReportClassGameModels = monthlyReportClassGameModels
            };

            int gameId = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == gamesBySubKind.Key).Id;

            monthlyReportSubKindGameModel = SetPlans(monthlyReportSubKindGameModel, new List<int> { gameId });

            return monthlyReportSubKindGameModel;
        }

        private MonthlyReportClassGameModel GetClassMonthlyReportModel(GameHuntPlanDto classHuntPlanDto)
        {
            var monthlyReportClassGameModel = new MonthlyReportClassGameModel
            {
                Class = classHuntPlanDto.Class,
                ClassName = GameClassXRefs.FirstOrDefault(x => x.Id == classHuntPlanDto.Class).ClassName
            };

            monthlyReportClassGameModel = SetPlans(monthlyReportClassGameModel, new List<int> { classHuntPlanDto.GameId }, classHuntPlanDto.Class);

            return monthlyReportClassGameModel;
        }
        
        private T SetPlans<T>(T model, List<int> gameIds, int? gameClass = null) where T : MonthlyReportGameBaseModel
        {
            if (gameClass.HasValue)
            {
                if (HuntPlans.Any(x => gameIds.Contains(x.GameId) && x.Class == gameClass))
                {
                    model.HuntPlanCulls = (int) HuntPlans.FirstOrDefault(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Cull;
                }

                model.Culls = HuntedGames.Count(x => gameIds.Contains(x.GameId) && x.GameClass == gameClass);
                model.Losses = LossGames.Count(x => gameIds.Contains(x.GameId) && x.Class == gameClass);
            }
            else
            {
                if (HuntPlans.Any(x => gameIds.Contains(x.GameId)))
                {
                    model.HuntPlanCulls = (int) HuntPlans.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Cull);
                }

                model.Culls = HuntedGames.Count(x => gameIds.Contains(x.GameId));
                model.Catches = CaughtGames.Where(x => gameIds.Contains(x.GameId)).Sum(x=> x.Count);
                model.Losses = LossGames.Count(x => gameIds.Contains(x.GameId));
            }

            return model;
        }
    }
}
