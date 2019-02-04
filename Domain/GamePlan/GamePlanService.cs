using System.Collections.Generic;
using System.Linq;
using Common.Enums;
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

        private IList<LossGameDto> _lossGames;
        private IList<LossGameDto> LossGames => _lossGames ?? (_lossGames = _lossGameDao.GetByMarketingYear(PreviousMarketingYear));

        private IList<LossGameDto> _sanitaryLossGames;
        private IList<LossGameDto> SanitaryLossGames => _sanitaryLossGames ?? (_sanitaryLossGames = _lossGameDao.GetSanitaryLossesByMarketingYear(PreviousMarketingYear));

        private IList<GameClassDto> _gameClassXRefs;
        private IList<GameClassDto> GameClassXRefs => _gameClassXRefs ?? (_gameClassXRefs = _gameClassDao.GetAll());

        private IList<GameCountFor10MarchDto> _gameCountsCountFor10March;
        private IList<GameCountFor10MarchDto> GameCountsGameCountFor10March => _gameCountsCountFor10March ?? (_gameCountsCountFor10March = _gameCountFor10MarchDao.GameCountBefore10MarchPlan(PreviousMarketingYear));

        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly ILossGameDao _lossGameDao;
        private readonly IGameClassDao _gameClassDao;
        private readonly IGameCountFor10MarchDao _gameCountFor10MarchDao;

        public GamePlanService() : this(new GameDao(), new GameHuntPlanDao(), new LossGameDao(), new HuntedGameDao(), new GameClassDao(), new GameCountFor10MarchDao())
        {
        }

        public GamePlanService(IGameDao gameDao, IGameHuntPlanDao gamePlanDao, ILossGameDao lossGameDao, IHuntedGameDao huntedGameDao, IGameClassDao gameClassDao, IGameCountFor10MarchDao countFor10MarchDao)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gamePlanDao;
            _lossGameDao = lossGameDao;
            _huntedGameDao = huntedGameDao;
            _gameClassDao = gameClassDao;
            _gameCountFor10MarchDao = countFor10MarchDao;
        }

        public AnnualPlanGameModel GetGameAnnualPlanModel(GameType gameType, int marketingYearId)
        {
            MarketingYearId = marketingYearId;
            GameType = (int) gameType;
            
            var gamesByKind = GamesByType.Where(x => x.Type == (int) gameType).GroupBy(x => x.Kind);

            var annualPlanKindGameModels = new List<AnnualPlanKindGameModel>();
            foreach (var gameByKind in gamesByKind)
            {
                if (!CurrentHuntPlans.Any(x => x.GameId == gameByKind.FirstOrDefault().Id))
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

        private AnnualPlanKindGameModel GetKindAnnualPlanModel(IGrouping<int, GameDto> currentHuntPlanForKind)
        {
            var annualPlanSubKindGameModels = new List<AnnualPlanSubKindGameModel>();

            foreach (var currentHuntPlanForSubKind in currentHuntPlanForKind.GroupBy(x => x.SubKind))
            {
                if (currentHuntPlanForSubKind.Key.HasValue)
                {
                    AnnualPlanSubKindGameModel subKindGameModel = GetSubKindAnnualPlanModel(currentHuntPlanForKind.Key, currentHuntPlanForSubKind);

                    annualPlanSubKindGameModels.Add(subKindGameModel);
                }
            }

            var annualPlanKindGameModel = new AnnualPlanKindGameModel
            {
                Kind = currentHuntPlanForKind.Key,
                KindName = GamesByType.FirstOrDefault(x => x.Kind == currentHuntPlanForKind.Key).KindName,
                Type = (GameType) GameType,
                AnnualPlanSubKindGameModels = annualPlanSubKindGameModels
            };

            List<int> gameIds = GamesByType.Where(x => x.Kind == currentHuntPlanForKind.Key).Select(x => x.Id).ToList();

            annualPlanKindGameModel = SetPlans(annualPlanKindGameModel, gameIds);
            
            return annualPlanKindGameModel;
        }

        private AnnualPlanSubKindGameModel GetSubKindAnnualPlanModel(int? gameKind, IGrouping<int?, GameDto> currentHuntPlanForSubKind)
        {
            if (!currentHuntPlanForSubKind.Key.HasValue)
            {
                return new AnnualPlanSubKindGameModel();
            }

            List<GameHuntPlanDto> classHuntPlans = (from game in Games
                                                    join currentHuntPlan in CurrentHuntPlans on game.Id equals currentHuntPlan.GameId
                                                    where game.Kind == gameKind && game.SubKind == currentHuntPlanForSubKind.Key && currentHuntPlan.Class != null
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
                SubKind = currentHuntPlanForSubKind.Key,
                SubKindName = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == currentHuntPlanForSubKind.Key).SubKindName,
                AnnualPlanClassGameModels = annualPlanClassGameModels
            };

            int gameId = GamesByType.FirstOrDefault(x => x.Kind == gameKind && x.SubKind == currentHuntPlanForSubKind.Key).Id;

            annualPlanKindGameModel = SetPlans(annualPlanKindGameModel, new List<int> {gameId});
            
            return annualPlanKindGameModel;
        }

        private AnnualPlanClassGameModel GetClassAnnualPlanModel(GameHuntPlanDto gameHuntPlanDto)
        {
            var annualPlanClassGameModel = new AnnualPlanClassGameModel
            {
                Class = gameHuntPlanDto.Class,
                ClassName = GameClassXRefs.FirstOrDefault(x => x.Id == gameHuntPlanDto.Class).ClassName
            };

            annualPlanClassGameModel = SetPlans(annualPlanClassGameModel, new List<int>{gameHuntPlanDto.GameId}, gameHuntPlanDto.Class);
            
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
