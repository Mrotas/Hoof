using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.Hunt;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Hoof.Controllers
{
    public class HuntController : Controller
    {
        private readonly IHuntService _huntService;

        public HuntController() : this(new HuntService())
        {
        }

        public HuntController(IHuntService huntService)
        {
            _huntService = huntService;
        }

        public ActionResult Index()
        {
            IList<HuntViewModel> huntModels = _huntService.GetAllHuntsForCurrentMarketingYear();
            return View(huntModels);
        }

        public ActionResult MyHunts()
        {
            HttpCookie userCookie = Request.Cookies["User"];
            if (userCookie == null)
            {
                return View(new List<HuntViewModel>());
            }

            string userIdString = userCookie["UserId"];
            if (!Int32.TryParse(userIdString, out int userId))
            {
                return View(new List<HuntViewModel>());
            }

            IList<HuntViewModel> myHunts = _huntService.GetCurrentMarketingYearHuntsByUserId(userId);
            return View(myHunts);
        }

        public JsonResult GetByMarketingYear(int marketingYearId)
        {
            IList<HuntViewModel> huntModels = _huntService.GetByMarketingYearId(marketingYearId);

            return Json(huntModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(HuntCreateModel model)
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

            _huntService.Create(model, userId);
            return Json(new {data = true}, JsonRequestBehavior.AllowGet);
        }
    }
}