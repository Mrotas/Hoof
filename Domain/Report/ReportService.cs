using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Extensions;
using DataAccess.Dao.Catch;
using DataAccess.Dao.Fodder;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.LossGame;
using DataAccess.Dto;
using Domain.Labor;
using Domain.Labor.Models;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Report.Models;
using Domain.Report.Models.Fodder;
using Domain.Report.Models.Game;
using Domain.Report.Models.Labor;
using Domain.Report.ViewModels;

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
        private readonly IFodderDao _fodderDao;
        private readonly ILaborService _laborService;

        public ReportService() : this(new GameDao(), 
            new GameHuntPlanDao(), 
            new HuntedGameDao(),
            new MarketingYearService(), 
            new GameClassDao(), 
            new LossGameDao(), 
            new CatchDao(), 
            new FodderDao(), 
            new LaborService())
        {
        }

        public ReportService(IGameDao gameDao, 
            IGameHuntPlanDao gameHuntPlanDao, 
            IHuntedGameDao huntedGameDao, 
            IMarketingYearService marketingYearService, 
            IGameClassDao gameClassDao, 
            ILossGameDao lossGameDao, 
            ICatchDao catchDao,
            IFodderDao fodderDao, 
            ILaborService laborService)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gameHuntPlanDao;
            _huntedGameDao = huntedGameDao;
            _marketingYearService = marketingYearService;
            _gameClassDao = gameClassDao;
            _lossGameDao = lossGameDao;
            _catchDao = catchDao;
            _fodderDao = fodderDao;
            _laborService = laborService;
        }

        public MonthlyReportViewModel GetMonthlyReportViewModel(DateTime startDate, DateTime endDate)
        {
            ReportDateFrom = startDate;
            ReportDateTo = endDate;
            MarketingYearId = _marketingYearService.GetMarketingYearByDate(ReportDateFrom);

            MarketingYearModel marketingYearModel = _marketingYearService.GetMarketingYearModel(MarketingYearId);

            var monthlyReportModel = new MonthlyReportViewModel
            {
                MonthlyReportBigGameModel = GetMonthlyReportData(GameType.Big),
                MonthlyReportSmallGameModel = GetMonthlyReportData(GameType.Small),
                MonthlyReportFodderModel = GetMonthlyReportFodderModel(),
                MonthlyReportLaborModel = GetMonthlyReportLaborModel(),
                ReportDateFrom = ReportDateFrom,
                ReportDateTo = ReportDateTo,
                MarketingYearModel = marketingYearModel
            };

            SetFallowDeerAndMouflonData(monthlyReportModel.MonthlyReportBigGameModel);

            return monthlyReportModel;
        }

        private MonthlyReportFodderModel GetMonthlyReportFodderModel()
        {
            IList<FodderDto> fodders = _fodderDao.GetByDateRange(ReportDateFrom, ReportDateTo);

            var monthlyReportFodderModel = new MonthlyReportFodderModel
            {
                Dry = GetMonthlyReportFodderTypeModel(FodderType.Dry, fodders),
                Juicy = GetMonthlyReportFodderTypeModel(FodderType.Juicy, fodders),
                Pithy = GetMonthlyReportFodderTypeModel(FodderType.Pithy, fodders),
                Salt = GetMonthlyReportFodderTypeModel(FodderType.Salt, fodders)
            };

            return monthlyReportFodderModel;
        }

        private MonthlyReportFodderTypeModel GetMonthlyReportFodderTypeModel(FodderType fodderType, IList<FodderDto> fodders)
        {
            var annualPlanFodderTypeModel = new MonthlyReportFodderTypeModel
            {
                FodderType = fodderType,
                FodderTypeName = TypeName.GetFodderTypeName((int)fodderType),
                PutOut = fodders.Where(x => x.Type == (int)fodderType).Sum(x => x.Kilograms)
            };

            return annualPlanFodderTypeModel;
        }

        private MonthlyReportLaborModel GetMonthlyReportLaborModel()
        {
            IList<LaborModel> laborModels = _laborService.GetLaborModels(ReportDateFrom, ReportDateTo);

            var monthlyReportLaborModel = new MonthlyReportLaborModel
            {
                LaborModels = laborModels
            };

            return monthlyReportLaborModel;
        }

        private MonthlyReportGameModel GetMonthlyReportData(GameType gameType)
        {
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

        private void SetFallowDeerAndMouflonData(MonthlyReportGameModel monthlyReportBigGameModel)
        {
            MonthlyReportKindGameModel mouflonReportModel = monthlyReportBigGameModel.MonthlyReportKindGameModels.FirstOrDefault(x => x.Kind == (int)GameKind.Mouflon);
            MonthlyReportKindGameModel fallowDeerReportModel = monthlyReportBigGameModel.MonthlyReportKindGameModels.FirstOrDefault(x => x.Kind == (int)GameKind.FallowDeer);
            if (mouflonReportModel == null || fallowDeerReportModel == null)
            {
                return;
            }

            fallowDeerReportModel.KindName += $"/{mouflonReportModel.KindName}";
            fallowDeerReportModel.Culls += mouflonReportModel.Culls;
            fallowDeerReportModel.Catches += mouflonReportModel.Catches;
            fallowDeerReportModel.Losses += mouflonReportModel.Losses;
            fallowDeerReportModel.HuntPlanCulls += mouflonReportModel.HuntPlanCulls;
            for (int i = 0; i < fallowDeerReportModel.MonthlyReportSubKindGameModels.Count; i++)
            {
                fallowDeerReportModel.MonthlyReportSubKindGameModels[i].SubKindName += $"/{mouflonReportModel.MonthlyReportSubKindGameModels[i].SubKindName}";
                fallowDeerReportModel.MonthlyReportSubKindGameModels[i].Culls += mouflonReportModel.MonthlyReportSubKindGameModels[i].Culls;
                fallowDeerReportModel.MonthlyReportSubKindGameModels[i].Catches += mouflonReportModel.MonthlyReportSubKindGameModels[i].Catches;
                fallowDeerReportModel.MonthlyReportSubKindGameModels[i].Losses += mouflonReportModel.MonthlyReportSubKindGameModels[i].Losses;
                fallowDeerReportModel.MonthlyReportSubKindGameModels[i].HuntPlanCulls += mouflonReportModel.MonthlyReportSubKindGameModels[i].HuntPlanCulls;
            }

            monthlyReportBigGameModel.MonthlyReportKindGameModels.Remove(monthlyReportBigGameModel.MonthlyReportKindGameModels.FirstOrDefault(x => x.Kind == (int)GameKind.Mouflon));
            monthlyReportBigGameModel.MonthlyReportKindGameModels.Remove(monthlyReportBigGameModel.MonthlyReportKindGameModels.FirstOrDefault(x => x.Kind == (int)GameKind.FallowDeer));

            monthlyReportBigGameModel.MonthlyReportKindGameModels.Add(fallowDeerReportModel);
            monthlyReportBigGameModel.MonthlyReportKindGameModels = monthlyReportBigGameModel.MonthlyReportKindGameModels.OrderBy(x => x.Kind).ToList();
        }
    }
}
