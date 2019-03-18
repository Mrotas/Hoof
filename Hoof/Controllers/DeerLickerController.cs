using System;
using System.Web.Mvc;
using Domain.DeerLicker;
using Domain.DeerLicker.ViewModels;

namespace Hoof.Controllers
{
    public class DeerLickerController : Controller
    {
        private readonly IDeerLickerService _deerLickerService;

        public DeerLickerController() : this(new DeerLickerService())
        {
        }

        public DeerLickerController(IDeerLickerService deerLickerService)
        {
            _deerLickerService = deerLickerService;
        }


        public ActionResult Index()
        {
            return View();
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
            return Json(new { message }, JsonRequestBehavior.AllowGet);
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
            return Json(new { message }, JsonRequestBehavior.AllowGet);
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
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}