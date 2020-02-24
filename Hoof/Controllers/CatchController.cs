using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.Catch;
using Domain.Catch.Models;
using Domain.Catch.ViewModels;

namespace Hoof.Controllers
{
    public class CatchController : Controller
    {
        private readonly ICatchService _catchService;

        public CatchController() : this(new CatchService())
        {
        }

        public CatchController(ICatchService catchService)
        {
            _catchService = catchService;
        }

        public ActionResult Index()
        {
            IList<CatchViewModel> catchViewModels = _catchService.GetAllCatchesForCurrentMarketingYear();
            return View(catchViewModels);
        }

        public ActionResult MyCaughts()
        {
            HttpCookie userCookie = Request.Cookies["User"];
            if (userCookie == null)
            {
                return View(new List<CatchViewModel>());
            }

            string userIdString = userCookie["UserId"];
            if (!Int32.TryParse(userIdString, out int userId))
            {
                return View(new List<CatchViewModel>());
            }

            IList<CatchViewModel> catchViewModels = _catchService.GetCurrentMarketingYearCatchesByUserId(userId);
            return View(catchViewModels);
        }

        [HttpPost]
        public JsonResult Create(CatchCreateModel model)
        {
            HttpCookie userCookie = Request.Cookies["User"];
            if (userCookie == null)
            {
                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }

            string userIdString = userCookie["UserId"];
            if (!Int32.TryParse(userIdString, out int userId))
            {
                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }

            _catchService.Create(model, userId);
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
    }
}