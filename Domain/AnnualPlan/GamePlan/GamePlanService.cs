using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.AnnualPlan.GameHuntPlan;
using DataAccess.Dao.Game;
using DataAccess.Dao.Hunt;
using DataAccess.Entities.AnnualPlan;
using Domain.AnnualPlan.GamePlan.Models;
using Domain.Game.Model;

namespace Domain.AnnualPlan.GamePlan
{
    public class GamePlanService : IGamePlanService
    {
        private IList<GameHuntPlan> _gameHuntPlans;
        private IList<DataAccess.Entities.Hunt> _gameHuntExecutions;

        private readonly IGameDao _gameDao;
        private readonly IGameHuntPlanDao _gameHuntPlanDao;
        private readonly IHuntDao _huntDao;

        public GamePlanService() : this(new GameDao(), new GameHuntPlanDao(), new HuntDao())
        {
            _gameHuntPlans = new List<GameHuntPlan>();
            _gameHuntExecutions = new List<DataAccess.Entities.Hunt>();
        }

        public GamePlanService(IGameDao gameDao, IGameHuntPlanDao gamePlanDao, IHuntDao huntDao)
        {
            _gameDao = gameDao;
            _gameHuntPlanDao = gamePlanDao;
            _huntDao = huntDao;
        }

        public IList<GamePlanModel> GetGamePlanModels(int year)
        {
            var gamePlanModels = new List<GamePlanModel>();

            IList<DataAccess.Entities.Game> allGames = _gameDao.GetAll();

            _gameHuntPlans = _gameHuntPlanDao.GetAll();

            _gameHuntExecutions = _huntDao.GetAll();

            foreach (DataAccess.Entities.Game game in allGames)
            {
                if (game.Type == 1)
                {
                    List<GamePlanModel> models = GetBigGameModel(year, game);
                    gamePlanModels.AddRange(models);
                }
                else
                {
                    GamePlanModel model = GetSmallGameModel(year, game);
                    gamePlanModels.Add(model);
                }
            }
            return gamePlanModels;
        }

        private List<GamePlanModel> GetBigGameModel(int year, DataAccess.Entities.Game game)
        {
            List<GamePlanModel> models = new List<GamePlanModel>();

            List<GameHuntPlanModel> currentGameHuntPlanModels = GetHuntPlanModel(year, game);

            foreach (GameHuntPlanModel currentGameHuntPlanModel in currentGameHuntPlanModels)
            {
                var bigGameModel = new GamePlanModel
                {
                    Class = currentGameHuntPlanModel.Class
                };

                bigGameModel.GameModel = new GameModel
                {
                    Id = game.Id,
                    Type = game.Type,
                    Kind = game.Kind,
                    KindName = game.KindName,
                    SubKind = game.SubKind,
                    SubKindName = game.SubKindName
                };

                List<GameHuntPlanModel> previousGameHuntPlanModels = GetHuntPlanModel(year - 1, game);

                bigGameModel.PreviousGameHuntPlan = previousGameHuntPlanModels.FirstOrDefault(x => x.Class == currentGameHuntPlanModel.Class);

                bigGameModel.PreviousGamePlanExecution = GetGamePlanExecution(year, game.Id, currentGameHuntPlanModel.Class);

                bigGameModel.CurrentGameHuntPlan = currentGameHuntPlanModel;

                bigGameModel.GameSettlementPlan = GetGameSettlementPlan(year, game.Id);

                bigGameModel.EstimatedGameCount = GetEstimatedGameCount(year, game.Id);

                models.Add(bigGameModel);
            }

            return models;
        }

        private GamePlanModel GetSmallGameModel(int year, DataAccess.Entities.Game game)
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

            smallGameModel.PreviousGameHuntPlan = GetHuntPlanModel(year - 1, game).FirstOrDefault();

            smallGameModel.PreviousGamePlanExecution = GetGamePlanExecution(year, game.Id, null);

            smallGameModel.CurrentGameHuntPlan = GetHuntPlanModel(year, game).FirstOrDefault();

            smallGameModel.GameSettlementPlan = GetGameSettlementPlan(year, game.Id);

            smallGameModel.EstimatedGameCount = GetEstimatedGameCount(year, game.Id);

            return smallGameModel;
        }

        private List<GameHuntPlanModel> GetHuntPlanModel(int year, DataAccess.Entities.Game game)
        {
            List<GameHuntPlanModel> gameHuntPlanModels = (from gameHuntPlan in _gameHuntPlans
                                                        where gameHuntPlan.GameId == game.Id &&
                                                              gameHuntPlan.Year == year
                                                        select new GameHuntPlanModel
                                                        {
                                                            GameId = game.Id,
                                                            Cull = gameHuntPlan.Cull,
                                                            Catch = gameHuntPlan.Catch,
                                                            Class = gameHuntPlan.Class
                                                        }).ToList();

            return gameHuntPlanModels;
        }
        private GameExecutionModel GetGamePlanExecution(int year, int gameId, int? gameClass)
        {
            List<DataAccess.Entities.Hunt> hunts = _gameHuntExecutions.Where(x => x.GameId == gameId && x.GameClass == gameClass && x.Date.Year == year).ToList();

            //TODO: Fill rest properties
            var model = new GameExecutionModel
            {
                Cull = hunts.Count
            };

            return model;
        }

        private GameSettlementPlanModel GetGameSettlementPlan(int year, int gameId)
        {
            //TODO: Fill model
            return new GameSettlementPlanModel();
        }

        private EstimatedGameCountModel GetEstimatedGameCount(int year, int gameId)
        {
            //TODO Fill model
            return new EstimatedGameCountModel();
        }
    }
}
