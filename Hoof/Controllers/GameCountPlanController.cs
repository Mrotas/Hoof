using System;
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

        [HttpPost]
        public JsonResult Add(GameCountPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _gameCountPlanService.AddGameHuntPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(GameCountPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _gameCountPlanService.UpdateGameHuntPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = String.Empty;
            try
            {
                _gameCountPlanService.DeleteGameHuntPlan(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}