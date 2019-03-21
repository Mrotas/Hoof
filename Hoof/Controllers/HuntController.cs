using System.Collections.Generic;
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
            IList<HuntViewModel> huntModels = _huntService.GetAllHunts();
            return View(huntModels);
        }

        public ActionResult MyHunts()
        {
            //TODO get huntsmanId
            IList<HuntViewModel> myHunts = _huntService.GetHuntsByHuntsmanId(1);
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
            //TODO get huntsmanId
            int huntsmanId = 1;
            _huntService.Create(model, huntsmanId);
            return Json(new {data = true}, JsonRequestBehavior.AllowGet);
        }
    }
}