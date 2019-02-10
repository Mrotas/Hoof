using System.Collections.Generic;
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
            IList<CatchViewModel> catchViewModels = _catchService.GetAllCatches();
            return View(catchViewModels);
        }

        public ActionResult MyCaughts()
        {
            //TODO get huntsmanId
            IList<CatchViewModel> catchViewModels = _catchService.GetCatchesByHuntsmanId(1);
            return View(catchViewModels);
        }

        [HttpPost]
        public JsonResult Create(CatchCreateModel model)
        {
            //TODO get huntsmanId
            int huntsmanId = 1;
            _catchService.Create(model, huntsmanId);
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
    }
}