using System.Collections.Generic;
using System.Web.Mvc;
using Domain.GameLoss;
using Domain.GameLoss.Model;
using Domain.GameLoss.ViewModel;

namespace Hoof.Controllers
{
    public class LossController : Controller
    {
        private readonly IGameLossService _gameLossService;

        public LossController() : this(new GameLossService())
        {
        }

        public LossController(IGameLossService gameLossService)
        {
            _gameLossService = gameLossService;
        }
        
        public ActionResult Index()
        {
            List<GameLossViewModel> lossGames = _gameLossService.GetAllLossGames();
            return View(lossGames);
        }

        public ActionResult ReportLoss(GameLossModel model)
        {
            _gameLossService.ReportLoss(model);

            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
    }
}