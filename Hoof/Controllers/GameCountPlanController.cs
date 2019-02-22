using System.Web.Mvc;
using Domain.GameCountPlan;
using Domain.GameCountPlan.ViewModels;

namespace Hoof.Controllers
{
    public class GameCountPlanController : Controller
    {
        private readonly IGameCountPlanService _gameCountPlanService;

        public GameCountPlanController() : this(new GameCountPlanService())
        {
            
        }

        public GameCountPlanController(IGameCountPlanService gameCountPlanService)
        {
            _gameCountPlanService = gameCountPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            CountPlanViewModel countPlanViewModel = _gameCountPlanService.GetCountPlanViewModel(marketingYearId);
            return View(countPlanViewModel);
        }
    }
}