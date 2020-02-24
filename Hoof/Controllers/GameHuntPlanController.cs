using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.GameHuntPlan;
using Domain.GameHuntPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class GameHuntPlanController : Controller
    {
        private readonly IGameHuntPlanService _gameHuntPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public GameHuntPlanController() : this(new GameHuntPlanService(), new MarketingYearService())
        {
        }

        public GameHuntPlanController(IGameHuntPlanService gameHuntPlanService, IMarketingYearService marketingYearService)
        {
            _gameHuntPlanService = gameHuntPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
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