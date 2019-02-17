using System.Web.Mvc;
using Domain.AnnualPlan;
using Domain.AnnualPlan.ViewModels;

namespace Hoof.Controllers
{
    public class AnnualPlanController : Controller
    {
        private readonly IAnnualPlanService _annualPlanService;

        public AnnualPlanController() : this(new AnnualPlanService())
        {
        }

        public AnnualPlanController(IAnnualPlanService annualPlanService)
        {
            _annualPlanService = annualPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            AnnualPlanViewModel annualPlanViewModel = _annualPlanService.GetAnnualPlanViewModel(marketingYearId);
            return View(annualPlanViewModel);
        }

        public JsonResult GetAnnualPlanJsonData(int marketingYearId)
        {
            AnnualPlanViewModel annualPlanViewModel = _annualPlanService.GetAnnualPlanViewModel(marketingYearId);
            return Json(annualPlanViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}