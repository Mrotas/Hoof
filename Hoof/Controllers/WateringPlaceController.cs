using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.WateringPlace;
using Domain.WateringPlace.ViewModels;

namespace Hoof.Controllers
{
    public class WateringPlaceController : Controller
    {
        private readonly IWateringPlaceService _wateringPlaceService;
        private readonly IMarketingYearService _marketingYearService;

        public WateringPlaceController() : this(new WateringPlaceService(), new MarketingYearService())
        {
        }

        public WateringPlaceController(IWateringPlaceService wateringPlace, IMarketingYearService marketingYearService)
        {
            _wateringPlaceService = wateringPlace;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            WateringPlaceBaseViewModel wateringPlaceBaseViewModel = _wateringPlaceService.GetWateringPlaceViewModel(marketingYearId);
            return View(wateringPlaceBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(WateringPlaceViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _wateringPlaceService.AddWateringPlace(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(WateringPlaceViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _wateringPlaceService.UpdateWateringPlace(model, marketingYearId);
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
                _wateringPlaceService.DeleteWateringPlace(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}