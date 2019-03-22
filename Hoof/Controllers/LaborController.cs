using System;
using System.Web.Mvc;
using Domain.Labor;
using Domain.Labor.ViewModels;

namespace Hoof.Controllers
{
    public class LaborController : Controller
    {
        private readonly ILaborService _laborService;

        public LaborController() : this(new LaborService())
        {
        }

        public LaborController(ILaborService laborService)
        {
            _laborService = laborService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int marketingYearId)
        {
            LaborBaseViewModel laborBaseViewModel = _laborService.GetLaborViewModel(marketingYearId);
            return View(laborBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(LaborViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _laborService.AddLabor(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(LaborViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _laborService.UpdateLabor(model, marketingYearId);
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
                _laborService.DeleteLabor(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}