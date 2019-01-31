using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Game;
using DataAccess.Dao.GameClass;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dao.GameLoss;
using DataAccess.Dao.Hunt;
using DataAccess.Dao.HuntedGame;
using DataAccess.Dto;
using Domain.AnnualPlan.Models.GamePlan;
using Domain.Game.Model;
using Domain.MarketingYear;

namespace Domain.GamePlan
{
    public class GamePlanService : IGamePlanService
    {
        private IList<GameHuntPlanDto> _gameHuntPlans;
        private IList<HuntDto> _gameHuntExecutions;
        private IList<HuntedGameDto> _huntedGames;
        private IList<GameLossDto> _lossGames;
        private IList<GameClassDto> _gameClasses;

        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntedGameDao _huntedGameDao;
        private readonly IHuntDao _huntDao;
        private readonly IGameLossDao _gameLossDao;
        private readonly IMarketingYearService _marketingYearService;
        private readonly IGameClassDao _gameClassDao;

        public GamePlanService() : this(new GameDao(), new GameHuntPlanDao(), new HuntDao(), new GameLossDao(), new HuntedGameDao(), new MarketingYearService(), new GameClassDao())
        {
            _gameHuntPlans = new List<GameHuntPlanDto>();
            _gameHuntExecutions = new List<HuntDto>();
            _huntedGames = new List<HuntedGameDto>();
            _lossGames = new List<GameLossDto>();
            _gameClasses = new List<GameClassDto>();
        }

        public GamePlanService(IGameDao gameDao, IGameHuntPlanDao gamePlanDao, IHuntDao huntDao, IGameLossDao gameLossDao, IHuntedGameDao huntedGameDao, IMarketingYearService marketingYearService, IGameClassDao gameClassDao)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gamePlanDao;
            _huntDao = huntDao;
            _gameLossDao = gameLossDao;
            _huntedGameDao = huntedGameDao;
            _marketingYearService = marketingYearService;
            _gameClassDao = gameClassDao;
        }

        public IList<GamePlanModel> GetGamePlanModels(int marketingYearId)
        {
            var gamePlanModels = new List<GamePlanModel>();

            IList<GameDto> allGames = _gameDao.GetAll();

            _gameHuntPlans = _gameHuntPlanDao.GetAll();

            _gameHuntExecutions = _huntDao.GetAll();

            _huntedGames = _huntedGameDao.GetAll();

            _lossGames = _gameLossDao.GetAll();

            _gameClasses = _gameClassDao.GetAll();

            foreach (GameDto game in allGames)
            {
                if (game.Type == 1)
                {
                    List<GamePlanModel> models = GetBigGameModel(marketingYearId, game);
                    gamePlanModels.AddRange(models);
                }
                else
                {
                    GamePlanModel model = GetSmallGameModel(marketingYearId, game);
                    gamePlanModels.Add(model);
                }
            }
            return gamePlanModels;
        }

        private List<GamePlanModel> GetBigGameModel(int marketingYearId, GameDto game)
        {
            List<GamePlanModel> models = new List<GamePlanModel>();

            List<GameHuntPlanModel> currentGameHuntPlanModels = GetHuntPlanModel(marketingYearId, game);

            foreach (GameHuntPlanModel currentGameHuntPlanModel in currentGameHuntPlanModels)
            {
                var bigGameModel = new GamePlanModel
                {
                    Class = currentGameHuntPlanModel.Class,
                    ClassName = GetClassName(currentGameHuntPlanModel.Class),
                    GameModel = new GameModel
                    {
                        Id = game.Id,
                        Type = game.Type,
                        Kind = game.Kind,
                        KindName = game.KindName,
                        SubKind = game.SubKind,
                        SubKindName = game.SubKindName
                    }
                };


                List<GameHuntPlanModel> previousGameHuntPlanModels = GetHuntPlanModel(marketingYearId - 1, game);

                bigGameModel.PreviousGameHuntPlan = previousGameHuntPlanModels.FirstOrDefault(x => x.Class == currentGameHuntPlanModel.Class) ?? new GameHuntPlanModel();

                bigGameModel.PreviousGamePlanExecution = GetGamePlanExecution(marketingYearId - 1, game.Id, currentGameHuntPlanModel.Class);

                bigGameModel.CurrentGameHuntPlan = currentGameHuntPlanModel;

                bigGameModel.GameSettlementPlan = GetGameSettlementPlan(marketingYearId, game.Id);

                bigGameModel.GameCountModel = GetGameCountModel(marketingYearId, game.Id);

                models.Add(bigGameModel);
            }

            return models;
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

        private GamePlanModel GetSmallGameModel(int marketingYearId, GameDto game)
        {
            var smallGameModel = new GamePlanModel();

            smallGameModel.GameModel = new GameModel
            {
                Id = game.Id,
                Type = game.Type,
                Kind = game.Kind,
                KindName = game.KindName,
                SubKind = game.SubKind,
                SubKindName = game.SubKindName
            };

            smallGameModel.PreviousGameHuntPlan = GetHuntPlanModel(marketingYearId - 1, game).FirstOrDefault();

            smallGameModel.PreviousGamePlanExecution = GetGamePlanExecution(marketingYearId, game.Id, null);

            smallGameModel.CurrentGameHuntPlan = GetHuntPlanModel(marketingYearId, game).FirstOrDefault();

            smallGameModel.GameSettlementPlan = GetGameSettlementPlan(marketingYearId, game.Id);

            smallGameModel.GameCountModel = GetGameCountModel(marketingYearId, game.Id);

            return smallGameModel;
        }

        private List<GameHuntPlanModel> GetHuntPlanModel(int marketingYear, GameDto game)
        {
            List<GameHuntPlanModel> gameHuntPlanModels = (from gameHuntPlan in _gameHuntPlans
                                                        where gameHuntPlan.GameId == game.Id && gameHuntPlan.MarketingYearId == marketingYear
                                                          select new GameHuntPlanModel
                                                        {
                                                            GameId = game.Id,
                                                            Cull = gameHuntPlan.Cull,
                                                            Catch = gameHuntPlan.Catch,
                                                            Class = gameHuntPlan.Class
                                                        }).ToList();

            return gameHuntPlanModels;
        }

        private GameExecutionModel GetGamePlanExecution(int marketingYearId, int gameId, int? gameClass)
        {
            //int culls = _gameHuntExecutions.Count(x => x.HuntedGameId == gameId && x.GameClass == gameClass && x.Date.Year == year);
            int culls = (from gameHuntExecution in _gameHuntExecutions
                        join huntedGame in _huntedGames on gameHuntExecution.HuntedGameId equals huntedGame.Id
                        where huntedGame.GameId == gameId && 
                              huntedGame.GameClass == gameClass && 
                              _marketingYearService.IsDateInMarketingYear(gameHuntExecution.Date, marketingYearId)
                        select gameHuntExecution.Id).Count();

            int gameLosses = _lossGames.Count(x => x.GameId == gameId);

            int sanitaryLosses = _lossGames.Count(x => x.GameId == gameId && x.SanitaryLoss);

            //TODO: Fill rest properties
            var model = new GameExecutionModel
            {
                Cull = culls,
                Loss = gameLosses,
                SanitaryLoss = sanitaryLosses
            };

            return model;
        }

        private GameSettlementPlanModel GetGameSettlementPlan(int year, int gameId)
        {
            //TODO: Fill model
            return new GameSettlementPlanModel();
        }

        private GameCountModel GetGameCountModel(int year, int gameId)
        {
            //TODO Fill model
            return new GameCountModel();
        }
    }
}
