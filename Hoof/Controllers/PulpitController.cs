using System;
using System.Web.Mvc;
using Domain.Pulpit;
using Domain.Pulpit.ViewModels;

namespace Hoof.Controllers
{
    public class PulpitController : Controller
    {
        private readonly IPulpitService _pulpitService;

        public PulpitController() : this(new PulpitService())
        {
        }

        public PulpitController(IPulpitService pulpitService)
        {
            _pulpitService = pulpitService;
        }

        public ActionResult Index()
        {
            return View();
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