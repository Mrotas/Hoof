using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Hunt;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Hoof.Controllers
{
    public class CaughtController : Controller
    {
        private readonly IHuntService _huntService;

        public CaughtController() : this(new HuntService())
        {
        }

        public CaughtController(IHuntService huntService)
        {
            _huntService = huntService;
        }

        public ActionResult Index()
        {
            IList<HuntViewModel> caughtModels = _huntService.GetAllCaughts();
            return View(caughtModels);
        }

        public ActionResult MyCaughts()
        {
            //TODO get huntsmanId
            IList<HuntViewModel> myHunts = _huntService.GetCaughtsByHuntsmanId(1);
            return View(myHunts);
        }

        [HttpPost]
        public JsonResult Create(HuntCreateModel model)
        {
            //TODO get huntsmanId
            int huntsmanId = 1;
            _huntService.Create(model, huntsmanId);
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
    }
}