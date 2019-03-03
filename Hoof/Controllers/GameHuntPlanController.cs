using System;
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

        [HttpPost]
        public JsonResult Add(GameHuntPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _gameHuntPlanService.AddGameHuntPlan(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(GameHuntPlanViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _gameHuntPlanService.UpdateGameHuntPlan(model, marketingYearId);
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
                _gameHuntPlanService.DeleteGameHuntPlan(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}