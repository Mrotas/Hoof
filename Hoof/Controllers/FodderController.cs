using System;
using System.Web.Mvc;
using Domain.Fodder;
using Domain.Fodder.ViewModels;

namespace Hoof.Controllers
{
    public class FodderController : Controller
    {
        private readonly IFodderService _fodderService;

        public FodderController() : this(new FodderService())
        {
        }

        public FodderController(IFodderService fodderService)
        {
            _fodderService = fodderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int marketingYearId)
        {
            FodderBaseViewModel fodderBaseView = _fodderService.GetFodderViewModel(marketingYearId);
            return View(fodderBaseView);
        }
        
        [HttpPost]
        public JsonResult Add(FodderViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderService.AddFodder(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(FodderViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _fodderService.UpdateFodder(model, marketingYearId);
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
                _fodderService.DeleteFodder(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}