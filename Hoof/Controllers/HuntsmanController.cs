using System.Collections.Generic;
using System.Web.Mvc;
using Domain.Huntsman;
using Domain.Huntsman.Models;

namespace Hoof.Controllers
{
    public class HuntsmanController : Controller
    {
        private readonly IHuntsmanService _huntsmanService;

        public HuntsmanController() : this(new HuntsmanService())
        {
        }

        public HuntsmanController(IHuntsmanService huntsmanService)
        {
            _huntsmanService = huntsmanService;
        }

        public JsonResult GetAll()
        {
            IList<HuntsmanModel> huntsmanModels = _huntsmanService.GetAll();

            return Json(huntsmanModels, JsonRequestBehavior.AllowGet);
        }
    }
}