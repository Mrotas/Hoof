using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Game;
using Domain.Game.Model;

namespace Hoof.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController() : this(new GameService())
        {
        }

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public JsonResult GetGameModels()
        {
            List<GameModel> gameModels = _gameService.GetAllGames();

            return Json(gameModels, JsonRequestBehavior.AllowGet);
        }
    }
}