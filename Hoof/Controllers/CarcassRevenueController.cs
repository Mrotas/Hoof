using System;
using System.Web.Mvc;
using Domain.CarcassRevenue;
using Domain.CarcassRevenue.ViewModels;

namespace Hoof.Controllers
{
    public class CarcassRevenueController : Controller
    {
        private readonly ICarcassRevenueService _carcassRevenueService;

        public CarcassRevenueController() : this(new CarcassRevenueService())
        {
        }

        public CarcassRevenueController(ICarcassRevenueService carcassRevenueService)
        {
            _carcassRevenueService = carcassRevenueService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int marketingYearId)
        {
            CarcassRevenueBaseViewModel carcassRevenueBaseViewModel = _carcassRevenueService.GetCarcassRevenueViewModel(marketingYearId);
            return View(carcassRevenueBaseViewModel);
        }

        [HttpPost]
        public JsonResult Add(CarcassRevenueViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _carcassRevenueService.AddCarcassRevenue(model, marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(CarcassRevenueViewModel model, int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _carcassRevenueService.UpdateCarcassRevenue(model, marketingYearId);
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
                _carcassRevenueService.DeleteCarcassRevenue(id);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }
    }
}