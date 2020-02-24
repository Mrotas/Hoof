using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataAccess.Dao.Catch;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameCountFor10March;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dao.LossGame;
using DataAccess.Dto;
using Domain.AnnualPlan.Models.GamePlan;

namespace Domain.GamePlan
{
    public class GamePlanService : IGamePlanService
    {
        private int MarketingYearId { get; set; }
        private int PreviousMarketingYear => MarketingYearId - 1;
        private int GameType { get; set; }

        private IList<GameDto> _games;
        private IList<GameDto> Games => _games ?? (_games = _gameDao.GetAll());
        private IList<GameDto> GamesByType => Games.Where(x => x.Type == GameType).ToList();

        private IList<GameHuntPlanDto> _currentHuntPlans;
        private IList<GameHuntPlanDto> CurrentHuntPlans => _currentHuntPlans ?? (_currentHuntPlans = _gameHuntPlanDao.GetByMarketingYear(MarketingYearId));

        private IList<GameHuntPlanDto> _previousHuntPlans;
        private IList<GameHuntPlanDto> PreviousHuntPlans => _previousHuntPlans ?? (_previousHuntPlans = _gameHuntPlanDao.GetByMarketingYear(PreviousMarketingYear));
        
        private IList<HuntedGameDto> _huntedGames;
        private IList<HuntedGameDto> HuntedGames => _huntedGames ?? (_huntedGames = _huntedGameDao.GetByMarketingYear(PreviousMarketingYear));

        private IList<CatchDto> _caughtGames;
        private IList<CatchDto> CaughtGames => _caughtGames ?? (_caughtGames = _catchDao.GetByMarketingYear(PreviousMarketingYear));

        private IList<LossGameDto> _lossGames;
        private IList<LossGameDto> LossGames => _lossGames ?? (_lossGames = _lossGameDao.GetByMarketingYear(PreviousMarketingYear));

        private IList<LossGameDto> _sanitaryLossGames;
        private IList<LossGameDto> SanitaryLossGames => _sanitaryLossGames ?? (_sanitaryLossGames = _lossGameDao.GetSanitaryLossesByMarketingYear(PreviousMarketingYear));

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private IList<GameCountFor10MarchDto> _gameCountsCountFor10March;
        private IList<GameCountFor10MarchDto> GameCountsGameCountFor10March => _gameCountsCountFor10March ?? (_gameCountsCountFor10March = _gameCountFor10MarchDao.GetByMarketingYear(PreviousMarketingYear));

        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly ICatchDao _catchDao;
        private readonly ILossGameDao _lossGameDao;
        private readonly IGameClassDao _gameClassDao;
        private readonly IGameCountFor10MarchDao _gameCountFor10MarchDao;

        public GamePlanService() : this(new GameDao(), new GameHuntPlanDao(), new LossGameDao(), new HuntedGameDao(), new GameClassDao(), new GameCountFor10MarchDao(), new CatchDao())
        {
        }

        public GamePlanService(IGameDao gameDao, IGameHuntPlanDao gamePlanDao, ILossGameDao lossGameDao, IHuntedGameDao huntedGameDao, IGameClassDao gameClassDao, IGameCountFor10MarchDao countFor10MarchDao, ICatchDao catchDao)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gamePlanDao;
            _lossGameDao = lossGameDao;
            _huntedGameDao = huntedGameDao;
            _gameClassDao = gameClassDao;
            _gameCountFor10MarchDao = countFor10MarchDao;
            _catchDao = catchDao;
        }

        public AnnualPlanGameModel GetGameAnnualPlanModel(GameType gameType, int marketingYearId)
        {
            MarketingYearId = marketingYearId;
            GameType = (int) gameType;
            
            var gamesByKind = GamesByType.GroupBy(x => x.Kind);

            var annualPlanKindGameModels = new List<AnnualPlanKindGameModel>();
            foreach (var gameByKind in gamesByKind)
            {
                if (!CurrentHuntPlans.Any(x => gameByKind.Any(y => y.Id == x.GameId)))
                {
                    continue;
                }
                AnnualPlanKindGameModel kindGameModel = GetKindAnnualPlanModel(gameByKind);

                annualPlanKindGameModels.Add(kindGameModel);
            }

            var gameAnnualPlanModel = new AnnualPlanGameModel
            {
                AnnualPlanKindGameModels = annualPlanKindGameModels
            };

            return gameAnnualPlanModel;
        }

        private AnnualPlanKindGameModel GetKindAnnualPlanModel(IGrouping<int, GameDto> gameByKind)
        {
            var annualPlanSubKindGameModels = new List<AnnualPlanSubKindGameModel>();

            foreach (var gameBySubKind in gameByKind.GroupBy(x => x.SubKind))
            {
                if (gameBySubKind.Key.HasValue)
                {
                    AnnualPlanSubKindGameModel subKindGameModel = GetSubKindAnnualPlanModel(gameByKind.Key, gameBySubKind);

                    annualPlanSubKindGameModels.Add(subKindGameModel);
                }
            }

            var annualPlanKindGameModel = new AnnualPlanKindGameModel
            {
                Kind = gameByKind.Key,
                KindName = GamesByType.FirstOrDefault(x => x.Kind == gameByKind.Key).KindName,
                Type = (GameType) GameType,
                AnnualPlanSubKindGameModels = annualPlanSubKindGameModels
            };

            List<int> gameIds = GamesByType.Where(x => x.Kind == gameByKind.Key).Select(x => x.Id).ToList();

            annualPlanKindGameModel = SetPlans(annualPlanKindGameModel, gameIds);
            
            return annualPlanKindGameModel;
        }

        private AnnualPlanSubKindGameModel GetSubKindAnnualPlanModel(int? gameKind, IGrouping<int?, GameDto> gameBySubKind)
        {
            if (!gameBySubKind.Key.HasValue)
            {
                return new AnnualPlanSubKindGameModel();
            }

            List<GameHuntPlanDto> classHuntPlans = (from game in Games
                                                    join currentHuntPlan in CurrentHuntPlans on game.Id equals currentHuntPlan.GameId
                                                    where game.Kind == gameKind && game.SubKind == gameBySubKind.Key && currentHuntPlan.Class != null
                                                    select currentHuntPlan).ToList();

            var annualPlanClassGameModels = new List<AnnualPlanClassGameModel>();
            foreach (GameHuntPlanDto classHuntPlan in classHuntPlans)
            {
                AnnualPlanClassGameModel annualPlanClassGameModel = GetClassAnnualPlanModel(classHuntPlan);

                annualPlanClassGameModels.Add(annualPlanClassGameModel);
            }

            var annualPlanKindGameModel = new AnnualPlanSubKindGameModel
            {
                Type = (GameType) GameType,
                SubKind = gameBySubKind.Key,
                SubKindName = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == gameBySubKind.Key).SubKindName,
                AnnualPlanClassGameModels = annualPlanClassGameModels
            };

            int gameId = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == gameBySubKind.Key).Id;

            annualPlanKindGameModel = SetPlans(annualPlanKindGameModel, new List<int> {gameId});
            
            return annualPlanKindGameModel;
        }

        private AnnualPlanClassGameModel GetClassAnnualPlanModel(GameHuntPlanDto classHuntPlanDto)
        {
            var annualPlanClassGameModel = new AnnualPlanClassGameModel
            {
                Class = classHuntPlanDto.Class,
                ClassName = GameClassXRefs.FirstOrDefault(x => x.Id == classHuntPlanDto.Class).ClassName
            };

            annualPlanClassGameModel = SetPlans(annualPlanClassGameModel, new List<int>{classHuntPlanDto.GameId}, classHuntPlanDto.Class);
            
            return annualPlanClassGameModel;
        }

        private T SetPlans<T>(T model, List<int> gameIds, int? gameClass = null) where T : AnnualPlanGameBaseModel
        {
            if (gameClass.HasValue)
            {
                if (PreviousHuntPlans.Any(x => gameIds.Contains(x.GameId) && x.Class == gameClass))
                {
                    model.PreviousHuntPlanCulls = (int) PreviousHuntPlans.FirstOrDefault(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Cull;
                    model.PreviousHuntPlanCatches = (int) PreviousHuntPlans.FirstOrDefault(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Catch;
                }

                model.PreviousHuntPlanExecutionCulls = HuntedGames.Count(x => gameIds.Contains(x.GameId) && x.GameClass == gameClass);
                model.PreviousHuntPlanExecutionLosses = LossGames.Count(x => gameIds.Contains(x.GameId) && x.Class == gameClass);
                model.PreviousHuntPlanExecutionSanitaryLosses = SanitaryLossGames.Count(x => gameIds.Contains(x.GameId) && x.Class == gameClass);

                if (GameCountsGameCountFor10March.Any(x => gameIds.Contains(x.GameId) && x.Class == gameClass))
                {
                    model.GameCountBefore10March = GameCountsGameCountFor10March.Where(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Sum(x => x.Count);
                }

                if (CurrentHuntPlans.Any(x => gameIds.Contains(x.GameId) && x.Class == gameClass))
                {
                    model.CurrentHuntPlanCulls = (int) CurrentHuntPlans.FirstOrDefault(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Cull;
                    model.CurrentHuntPlanCatches = (int) CurrentHuntPlans.FirstOrDefault(x => gameIds.Contains(x.GameId) && x.Class == gameClass).Catch;
                }
            }
            else
            {
                if (PreviousHuntPlans.Any(x => gameIds.Contains(x.GameId)))
                {
                    model.PreviousHuntPlanCulls = (int) PreviousHuntPlans.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Cull);
                    model.PreviousHuntPlanCatches = (int) PreviousHuntPlans.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Catch);
                }

                model.PreviousHuntPlanExecutionCulls = HuntedGames.Count(x => gameIds.Contains(x.GameId));
                model.PreviousHuntPlanExecutionCatches = CaughtGames.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Count);
                model.PreviousHuntPlanExecutionLosses = LossGames.Count(x => gameIds.Contains(x.GameId));
                model.PreviousHuntPlanExecutionSanitaryLosses = SanitaryLossGames.Count(x => gameIds.Contains(x.GameId));

                if (GameCountsGameCountFor10March.Any(x => gameIds.Contains(x.GameId)))
                {
                    model.GameCountBefore10March = GameCountsGameCountFor10March.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Count);
                }

                if (CurrentHuntPlans.Any(x => gameIds.Contains(x.GameId)))
                {
                    model.CurrentHuntPlanCulls = (int) CurrentHuntPlans.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Cull);
                    model.CurrentHuntPlanCatches = (int) CurrentHuntPlans.Where(x => gameIds.Contains(x.GameId)).Sum(x => x.Catch);
                }
            }

            return model;
        }
    }
}
