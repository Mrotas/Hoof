using System.Web.Mvc;
using Domain.GameHuntPlan;
using Domain.GameHuntPlan.ViewModels;

namespace Hoof.Controllers
{
    public class GameHuntPlanController : Controller
    {
        private readonly IGameHuntPlanService _gameHuntPlanService;

        public GameHuntPlanController() : this(new GameHuntPlanService())
        {
        }

        public GameHuntPlanController(IGameHuntPlanService gameHuntPlanService)
        {
            _gameHuntPlanService = gameHuntPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            HuntPlanViewModel huntPlanViewModel = _gameHuntPlanService.GetHuntPlanViewModel(marketingYearId);
            return View(huntPlanViewModel);
        }
    }
}