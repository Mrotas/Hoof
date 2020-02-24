using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.GameCountPlan;
using Domain.GameCountPlan.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class GameCountPlanController : Controller
    {
        private readonly IGameCountPlanService _gameCountPlanService;
        private readonly IMarketingYearService _marketingYearService;

        public GameCountPlanController() : this(new GameCountPlanService(), new MarketingYearService())
        {
            
        }

        public GameCountPlanController(IGameCountPlanService gameCountPlanService, IMarketingYearService marketingYearService)
        {
            _gameCountPlanService = gameCountPlanService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
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