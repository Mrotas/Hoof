using System;
using System.Web.Mvc;
using Domain.Pasture;
using Domain.Pasture.ViewModels;

namespace Hoof.Controllers
{
    public class PastureController : Controller
    {
        private readonly IPastureService _pastureService;

        public PastureController() : this(new PastureService())
        {
        }

        public PastureController(IPastureService pastureService)
        {
            _pastureService = pastureService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int marketingYearId)
        {
            PastureBaseViewModel fodderBaseView = _pastureService.GetPastureViewModel(marketingYearId);
            return View(fodderBaseView);
        }

        [HttpPost]
        public JsonResult Add(PastureViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pastureService.AddPasture(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(PastureViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _pastureService.UpdatePasture(model, marketingYearId);
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
                _pastureService.DeletePasture(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}