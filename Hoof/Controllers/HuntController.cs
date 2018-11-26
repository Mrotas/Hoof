using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Game;
using Domain.Game.Model;
using Domain.Hunt;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Hoof.Controllers
{
    public class HuntController : Controller
    {
        private readonly IHuntService _huntService;
        private readonly IGameService _gameService;

        public HuntController() : this(new HuntService(), new GameService())
        {
        }

        public HuntController(IHuntService huntService, IGameService gameService)
        {
            _huntService = huntService;
            _gameService = gameService;
        }

        public ActionResult Index()
        {
            IList<HuntViewModel> huntModels = _huntService.GetAllHuntModels();
            return View(huntModels);
        }

        public ActionResult MyHunts()
        {
            IList<HuntViewModel> myHunts = _huntService.GetHuntViewModels(1);
            return View(myHunts);
        }
        
        [HttpPost]
        public JsonResult Create(HuntCreateModel model)
        {
            //TODO get huntsmanId
            int huntsmanId = 1;
            _huntService.Create(model, huntsmanId);
            return Json(new {data = true}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGameList()
        {
            List<GameModel> gameModels = _gameService.GetAllGames();

            return Json(gameModels, JsonRequestBehavior.AllowGet);
        }
    }
}