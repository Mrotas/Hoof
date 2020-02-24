using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.MarketingYear;
using Domain.MarketingYear.Models;
using Domain.Pulpit;
using Domain.Pulpit.ViewModels;

namespace Hoof.Controllers
{
    public class PulpitController : Controller
    {
        private readonly IPulpitService _pulpitService;
        private readonly IMarketingYearService _marketingYearService;

        public PulpitController() : this(new PulpitService(), new MarketingYearService())
        {
        }

        public PulpitController(IPulpitService pulpitService, IMarketingYearService marketingYearService)
        {
            _pulpitService = pulpitService;
            _marketingYearService = marketingYearService;
        }

        public ActionResult Index()
        {
            IList<MarketingYearModel> marketingYearModels = _marketingYearService.GetAll();
            return View(marketingYearModels);
        }

        public ActionResult List(int marketingYearId)
        {
            PulpitBaseViewModel pulpitBaseViewModel = _pulpitService.GetPulpitViewModel(marketingYearId);
            return View(pulpitBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(PulpitViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pulpitService.AddPulpit(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(PulpitViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pulpitService.UpdatePulpit(model, marketingYearId);
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
                _pulpitService.DeletePulpit(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}