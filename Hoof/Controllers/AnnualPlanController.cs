using System.Web.Mvc;
using Domain.AnnualPlan;
using Domain.AnnualPlan.Service;
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
            AnnualPlanViewModel annualPlanViewModel = _annualPlanService.GetAnnualPlanViewModel();
            return View(annualPlanViewModel);
        }
    }
}