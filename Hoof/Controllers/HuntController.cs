using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Hunt;
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
            IList<HuntViewModel> huntModels = _huntService.GetAllHuntModels();
            return View(huntModels);
        }

        public ActionResult MyHunts()
        {
            IList<HuntViewModel> myHunts = _huntService.GetHuntViewModels(1);
            return View(myHunts);
        }
    }
}