using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.DeerLicker;
using Domain.DeerLicker.ViewModels;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;

namespace Hoof.Controllers
{
    public class DeerLickerController : Controller
    {
        private readonly IDeerLickerService _deerLickerService;
        private readonly IMarketingYearService _marketingYearService;

        public DeerLickerController() : this(new DeerLickerService(), new MarketingYearService())
        {
        }

        public DeerLickerController(IDeerLickerService deerLickerService, IMarketingYearService marketingYearService)
        {
            _deerLickerService = deerLickerService;
            _marketingYearService = marketingYearService;
        }


        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            DeerLickerBaseViewModel deerLickerBaseViewModel = _deerLickerService.GetDeerLickerViewModel(marketingYearId);
            return View(deerLickerBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(DeerLickerViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _deerLickerService.AddDeerLicker(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new {message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(DeerLickerViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _deerLickerService.UpdateDeerLicker(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new {message}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = String.Empty;
            try
            {
                _deerLickerService.DeleteDeerLicker(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(new {message}, JsonRequestBehavior.AllowGet);
        }
    }
}